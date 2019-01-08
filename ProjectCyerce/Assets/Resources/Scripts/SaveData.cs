using System.Collections.Generic;

public class SaveData {

    private Dictionary<string, string> _dictionaryData;
    readonly string PATH = @"./save.txt";

    public SaveData()
    {
        _dictionaryData = new Dictionary<string, string>();
    }

    public bool Save()
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(PATH))
            {
                foreach (var d in _dictionaryData)
                {
                    file.WriteLine(d.Key + "=" + d.Value);
                }
            }
        }
        catch (System.Exception e)
        {
            return false;
        }

        return true;
    }

    public bool Load()
    {
        _dictionaryData = new Dictionary<string, string>();
        string line = "";
        try
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(PATH))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var split = line.Split('=');
                    SetDataItem(split[0], split[1], false);
                }
            }
        }
        catch (System.Exception e)
        {
            return false;
        }
        return true;
    }

    public bool Erase()
    {
        _dictionaryData = new Dictionary<string, string>();
        return true;
    }

    public bool SetDataItem(string key, string val, bool overwrite)
    {
        if(ContainsKey(key))
        {
            if(!overwrite)
            {
                return false;
            }
            _dictionaryData.Remove(key);
        }

        _dictionaryData.Add(key, val);

        return true;
    }

    public bool SetDataItem(string key, int val, bool overwrite)
    {
        return SetDataItem(key, val.ToString(), overwrite);
    }

    public bool SetData(string key, float val, bool overwrite)
    {
        return SetDataItem(key, val.ToString(), overwrite);
    }

    public string GetDataItem(string key)
    {
        if(ContainsKey(key))
        {
            string o;
            if(_dictionaryData.TryGetValue(key, out o))
            {
                return o;
            }
        }
        return null;
    }

    public bool ContainsKey(string key)
    {
        return _dictionaryData.ContainsKey(key);
    }

    // PRIVATE --------------------------------------------------------------------------

    private bool LoadData(string path)
    {

        return false;
    }

    // OLD --------------------------------------------------------------------------

    public void LoadDataFromFile()
    {
        var raw_lines = LoadDataLines();
        var meta = ParseData(raw_lines);
    }

    public void DevSaveCreate()
    {
        // ONLY USE THEIS TO CREATE A NEW DEV SAVE FILE. CREATES A BLANK FILE THAT NEEDS PRIMED...
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(PATH))
        {
            file.WriteLine("");
        }
    }

    public string[] LoadDataLines()
    {
        List<string> aLines = new List<string>();
        string[] lines;
        string line;
        //print("read save file");
        using (System.IO.StreamReader file = new System.IO.StreamReader(PATH))
        {
            while ((line = file.ReadLine()) != null)
            {
                aLines.Add(line);
                //print(line);
            }
        }

        lines = aLines.ToArray();
        return lines;
    }

    private Dictionary<string, string>[] ParseData(string[] lines)
    {
       
        var playersdata = new Dictionary<string, string>[5];
        int index = 0;

        foreach(string single_line in lines)
        {
            var dic = new Dictionary<string, string>();
            var split = single_line.Split(';');
            foreach(string data_item in split)
            {
                // TODO --------------------------------------------------------
                var key_val = data_item.Split('=');
                dic.Add(key_val[0], key_val[1]);
            }

            playersdata[index] = dic;
            index++;
        }

        return playersdata;
    }
}
