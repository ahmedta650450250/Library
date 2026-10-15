using System;
using System.IO;
using System.Text.Json;

namespace Library;



public class SettingsProvider
{
    private readonly string _filePath;
  
    public SettingsProvider(string filepath)
    {
      _filePath = filepath;
    }

    

    
    public LibrarySettings Load()
    {

      if (!File.Exists(_filePath))
      return new LibrarySettings();

      
      var json = File.ReadAllText(_filePath);
      var data = JsonSerializer.Deserialize<LibrarySettings>(json);

      
      if(data == null)
        return new LibrarySettings();

      data.Validate();
      return data;
    }
}
