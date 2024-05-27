using System.IO;
static void Main(string[] args) {
    Dictionary<int, Cell> dict = new Dictionary<int, Cell>();
    //step one: read the file
    IEnumerable<string> rows = File.ReadLines("cells.csv");
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
            // Console.WriteLine(row);
            Console.WriteLine("[");
            foreach (string item in origs) {Console.WriteLine(item + "---");}
            Console.WriteLine("]");
            
            //cleaning time
            //done: oem, model, announceYear
            //to do: status, dimensions, weight, sim, type, size, res, feat, os
            string? oem = cleanMissingValue(origs[0]);
            string? model = cleanMissingValue(origs[1]);
            // Console.WriteLine(origs[2]);
            int? announceYear = cleanYear(origs[2]);
            // Console.WriteLine(announceYear);
            // Console.WriteLine(oem);
            Cell c = new Cell(row.Split(","));
            dict.Add(lineNum, c);
        }
        lineNum++;
    }
    Console.WriteLine("Done");
}

static string? cleanMissingValue(string orig) {
    if (String.IsNullOrEmpty(orig) || orig.Equals("-")) {
        return null;
    } else {return orig;}
}

static string? getNext(string row, int begin) {
    int length = 0;
    if (row.IndexOf(",", begin) == begin) return null;
    if (row.IndexOf("\"", begin) == begin) {
        begin++;
        length = row.IndexOf("\"", begin) - begin;
    } else {
        length = row.IndexOf(",", begin) - begin;
        if (length < 0) length = row.Length - begin;
    }
    return row.Substring(begin, length);
}

static int? cleanYear(string orig) {
    int index = orig.IndexOf(",");
    int result = -1;
    if (index >= 0 && int.TryParse(orig.Substring(0, index), out result)) {
        return result;
    }
    return null;
}

Main(null);
