using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

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
                    int? announceYear = cleanYear(origs[2]);
                    string? launchStatus = cleanStatus(origs[3]);
                    string? bodyDim = cleanMissingValue(origs[4]);
                    float? weight = cleanWeight(origs[5]);
                    string? sim = cleanYesNo(cleanMissingValue(origs[6]));
                    string? displayType = cleanMissingValue(origs[7]);
                    float? inches = cleanSize(origs[8]);
                    string? res = cleanMissingValue(origs[9]);
                    string? features = cleanNums(origs[10]);
                    string? os = cleanOS(origs[11]);

                    //create each cell and add to dict
                    Cell c = new Cell(oem, model, announceYear, launchStatus, bodyDim, weight, sim, displayType, inches, res, features, os);
                    dict.Add(lineNum, c);
                    Console.WriteLine("Line " + lineNum + ": " + c);
                }
                lineNum++;
            }
            Console.WriteLine("Done");
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
        
        string getOemHighAvgBodyWeight(Dictionary<int, Cell> dict) {
            //for each line
            //get the oem
            //add the body weight to the sum for that oem and increment count for that oem
            //for each final weight, divide by count, return oem with largest
            foreach (KeyValuePair<int, Cell> entry in dict) {
                Console.WriteLine(entry.Value);
            }
            return "bees";
        }
    }

}