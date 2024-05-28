using System.Buffers;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Program {
    public class Program {

        static void Main(string[] args) {
            Dictionary<int, Cell> dict = new Dictionary<int, Cell>();
            //step one: read the file
            IEnumerable<string> rows = File.ReadLines("src/cells.csv");
            int lineNum = 1;
            //step two: create the Cell objects
            foreach (string row in rows) {
                if (lineNum > 1) {
                    string?[] origs = new string[12];

                    int begin = 0;
                    int length = 0;
                    for (int index = 0; index < 12; index++) {
                        //get the next field
                        origs[index] = getNext(row, begin);
                        //if it was in quotes, add 2 to the length, plus 1 for the comma seperator
                        if (row.IndexOf("\"", begin) == begin) length = 3;
                        else length = 1;
                        begin += length;
                        if (origs[index] != null) begin += origs[index].Length;
                    }            
                    
                    //cleaning time
                    string? oem = cleanMissingValue(origs[0]);
                    string? model = cleanMissingValue(origs[1]);
                    int? announceYear = cleanYear(cleanMissingValue(origs[2]));
                    string? launchStatus = cleanStatus(cleanMissingValue(origs[3]));
                    string? bodyDim = cleanMissingValue(origs[4]);
                    float? weight = cleanWeight(cleanMissingValue(origs[5]));
                    string? sim = cleanYesNo(cleanMissingValue(origs[6]));
                    string? displayType = cleanMissingValue(origs[7]);
                    float? inches = cleanSize(cleanMissingValue(origs[8]));
                    string? res = cleanMissingValue(origs[9]);
                    string? features = cleanNums(cleanMissingValue(origs[10]));
                    string? os = cleanOS(cleanMissingValue(origs[11]));

                    //create each cell and add to dict
                    Cell c = new Cell(oem, model, announceYear, launchStatus, bodyDim, weight, sim, displayType, inches, res, features, os);
                    //only add if not a duplicate row
                    if (! dict.ContainsValue(c)) dict.Add(lineNum, c);
                    
                }
                lineNum++;
            }
            Console.WriteLine("Done");
            Console.WriteLine("OEM with max body weight is: " + getOemHighAvgBodyWeight(dict));
            Console.WriteLine("\nThese models were announced in one year and released in another:");
            Console.WriteLine(getYearDifferentOemModels(dict));
            Console.WriteLine("There are " + numPhonesWith1Feature(dict) + " phones with 1 feature.\n");
            Console.WriteLine("The year with the most releases after 1999 so far was " + yearMostReleases2000Up(dict));
        }

        public static string? cleanMissingValue(string? orig) {
            if (orig != null) {
                string trimmed = orig.Trim();
                if (trimmed.Equals("-") || trimmed.Equals("")) return null;
                else return trimmed;
            }
            return null;
        }

        static string? getNext(string row, int begin) {
            int length = 0;
            if (row.IndexOf(',', begin) == begin) return null;
            if (row.IndexOf('\"', begin) == begin) {
                begin++;
                length = row.IndexOf('\"', begin) - begin;
            } else {
                length = row.IndexOf(',', begin) - begin;
                if (length < 0) length = row.Length - begin;
            }
            return row.Substring(begin, length);
        }

        public static int? cleanYear(string? orig) {
            if (orig != null) {
                string pattern = "[0-9]{4}";
                if (Regex.IsMatch(orig, pattern)) {
                    return int.Parse(Regex.Match(orig, pattern).Value);
                }
            }
            return null;
        }

        public static string? cleanStatus(string? orig) {
            if (orig != null) {
                string? result = orig.Trim();
                if (result.Equals("Discontinued") || result.Equals("Canceled")) {
                    return result;
                } else {
                    string reg = "Available. Released [0-9]{4}";
                    if (Regex.IsMatch(result, reg)) {
                        return result;
                    }
                }
            }
            return null;
        }

        public static float? cleanWeight(string? orig) {
            if (orig != null) {
                int endIndex = orig.IndexOf('g');
                if (endIndex > 0) {
                    string result = orig.Substring(0, endIndex).Trim();
                    float f;
                    try {
                        f = float.Parse(result);
                    } catch (Exception) {
                        return null;
                    }
                    return f;
                }
            }
            return null;
        }
        
        public static string? cleanYesNo(string? orig) {
            if (orig != null) {
                if (! orig.Equals("Yes") && ! orig.Equals("No")) return orig;
            }
            return null;
        }
        
        public static float? cleanSize(string? orig) {
            if (orig != null) {
                string pattern = "[0-9]+.[0-9]+ inches";
                if (Regex.IsMatch(orig, pattern)) {
                    Match m = Regex.Match(orig, pattern);
                    string result = m.Value.Substring(0, m.Value.IndexOf("inches")).Trim();
                    return float.Parse(result);
                }
            }
            return null;
        }

        public static string? cleanNums(string? orig) {
            if (orig != null) {
                string? result = cleanMissingValue(orig);
                if (result != null && Regex.IsMatch(result, "[a-zA-Z]")) {
                    return result;
                }
            }
            return null;
        }

        public static string? cleanOS(string? orig) {
            if (orig != null) {
                int commaIdx = orig.IndexOf(',');
                if (commaIdx < 0) return orig;
                else {return orig[..commaIdx].Trim();}
            }
            return null;
        }
        
        public static string getOemHighAvgBodyWeight(Dictionary<int, Cell> dict) {
            //to hold the numbers
            Dictionary<string, Tuple<float?, int>> d = new Dictionary<string, Tuple<float?, int>>();
            //for each line get the oem
            foreach (KeyValuePair<int, Cell> entry in dict) {
                if (entry.Value.Oem != null) {
                    if (d.ContainsKey(entry.Value.Oem)) {
                    //add the body weight to the sum for that oem and increment count for that oem
                        float? newSum = entry.Value.BodyWeight;
                        float? oldSum = d[entry.Value.Oem].Item1;
                        if (newSum != null) oldSum += newSum;
                        int newCount = d[entry.Value.Oem].Item2 + 1;
                        d[entry.Value.Oem] = new Tuple<float?, int>(oldSum, newCount);
                    } else {
                        float? sum = entry.Value.BodyWeight;
                        if (sum != null) d[entry.Value.Oem] = new Tuple<float?, int>(sum, 1);
                    }
                }
            //for each final weight, divide by count, return oem with largest
            }
            string result = "";
            float? max = 0;
            float? avg = 0;
            foreach (KeyValuePair<string, Tuple<float?, int>> pair in d) {
                if (pair.Value.Item1 != null && pair.Value.Item2 != 0) {
                    avg = pair.Value.Item1 / pair.Value.Item2;
                    if (avg > max) {
                        max = avg;
                        result = pair.Key;
                    }
                }
            }
            return result + " with an average body weight of " + avg;
        }

        public static string getYearDifferentOemModels (Dictionary<int, Cell> dict) {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, Cell> pair in dict) {
                string? ls = pair.Value.LaunchStatus;
                //if the status starts with available
                if (ls != null && ls.StartsWith("Av")) {
                    int? year = cleanYear(ls);
                    //and the announceYear != status year extracted
                    if (year != null && year != pair.Value.LaunchAnnounced) {
                        //add oem and model and newline to result
                        sb.Append(pair.Value.Oem).Append(' ').Append(pair.Value.Model).Append('\n');
                    }
                }
            }
            return sb.ToString();
        }

        public static int numPhonesWith1Feature(Dictionary<int, Cell> dict) {
            int count = 0;
            foreach (KeyValuePair<int, Cell> pair in dict) {
                string? features = pair.Value.FeaturesSensors;
                if (features != null && ! features.Contains(',')) {
                    count++;
                }
            }
            return count;
        }

        public static int yearMostReleases2000Up(Dictionary<int, Cell> dict) {
            Dictionary<int, int> yearCount = new Dictionary<int, int>();
            foreach (KeyValuePair<int, Cell> pair in dict) {
                string? status = pair.Value.LaunchStatus;
                if (status != null && status.StartsWith("Av")) {
                    int? year = cleanYear(status);
                    if (year != null && year > 1999) {
                        if (yearCount.ContainsKey((int)year)) yearCount[(int)year]++;
                        else {yearCount[(int)year] = 1;}
                    }
                }
            }
            int max = 0;
            int topYear = 0;
            foreach (KeyValuePair<int, int> entry in yearCount) {
                if (entry.Value > max) topYear = entry.Key;
            }
            return topYear;
        }

        //Seven methods:
            //1 - Cell.toString
            //2 - Cell.Equals
            //3 - First Ever Launch Announcement Year (Min)
            //4 - Middle Year of All Launch Announcements (Median)
            //5 - Average Display Size (Mean)
            //6 - Heaviest Body Weight (Max)
            //7 - Most Frequent sim card (Mode)

        public static int getFirstLaunchYear(Dictionary<int, Cell> dict) {
            int year = 9999;
            foreach (KeyValuePair<int, Cell> pair in dict) {
                int? y = pair.Value.LaunchAnnounced;
                if (y != null && y < year) year = (int)y;
            }
            return year;
        }

        public static double getMedianLaunchYear(Dictionary<int, Cell> dict) {
            int[] years = new int[dict.Count];
            int index = 0;
            foreach (KeyValuePair<int, Cell> pair in dict) {
                int? year = pair.Value.LaunchAnnounced;
                if (year != null) {
                    years[index] = (int)year;
                }
            }
            Array.Sort(years);
            if (years.Length % 2 != 0) {
                return years[years.Length / 2];
            } else {
                return (years[years.Length / 2] + years[years.Length / 2 - 1]) / 2.0;
            }
        }

        public static float getMeanDisplaySize(Dictionary<int, Cell> dict) {
            float total = 0;
            int count = 0;
            foreach (Cell value in dict.Values) {
                if (value.DisplaySize != null) {
                    total += (float)value.DisplaySize;
                    count++;
                }
            }
            return total / count;
        }

        public static float getMaxBodyWeight(Dictionary<int, Cell> dict) {
            float max = 0;
            foreach (Cell value in dict.Values) {
                if (value.BodyWeight != null && value.BodyWeight > max) max = (float)value.BodyWeight; 
            }
            return max;
        }

        public static string simCardMode(Dictionary<int, Cell> dict) {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (Cell c in dict.Values) {
                if (counts.ContainsKey(c.BodySim)) {
                    counts[c.BodySim]++;
                } else {
                    counts[c.BodySim] = 1;
                }
            }
            string mode = "";
            int max = 0;
            foreach (KeyValuePair<string, int> pair in counts) {
                if (pair.Value > max) {
                    mode = pair.Key;
                    max = pair.Value;
                }
            }
            return mode + " occurred most often at " + max + " times.";
        }
    }
}