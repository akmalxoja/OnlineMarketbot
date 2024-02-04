using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class GenericCRUDService<T> where T : class
{
    private readonly string filePath;

    public GenericCRUDService(string filePath)
    {
        this.filePath = filePath;
    }

    public void Create(T newData)
    {
        List<T> dataList = Read();
        dataList.Add(newData);
        Write(dataList);
        Console.WriteLine("Data added successfully.");
    }

    public List<T> Read()
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
            return new List<T>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON format.");
            return new List<T>();
        }
    }

    public void Update(Predicate<T> predicate, Action<T> updateAction)
    {
        List<T> dataList = Read();
        var existingData = dataList.Find(predicate);
        if (existingData != null)
        {
            updateAction(existingData);
            Write(dataList);
            Console.WriteLine("Data updated successfully.");
        }
        else
        {
            Console.WriteLine("Data not found!");
        }
    }

    public void Delete(Predicate<T> predicate)
    {
        List<T> dataList = Read();
        var dataToRemove = dataList.Find(predicate);
        if (dataToRemove != null)
        {
            dataList.Remove(dataToRemove);
            Write(dataList);
            Console.WriteLine("Data deleted successfully.");
        }
        else
        {
            Console.WriteLine("Data not found!");
        }
    }


    private void Write(List<T> dataList)
    {
        string json = JsonSerializer.Serialize(dataList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}
