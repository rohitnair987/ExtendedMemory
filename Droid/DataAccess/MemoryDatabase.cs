﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Couchbase.Lite;
using ExtendedMemory.DataAccess;
using ExtendedMemory.Models;
using Xamarin.Forms;


[assembly: Dependency(typeof(MemoryDatabase))]
namespace ExtendedMemory.DataAccess
{
    public class MemoryDatabase : IMemoryDatabase
    {
        static Database database;

        public MemoryDatabase()
        {
            Couchbase.Lite.Storage.SystemSQLite.Plugin.Register();
            database = Manager.SharedInstance.GetDatabase("extendedmemory");
        }

        public Response<string> Save(Memory memory)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            foreach (PropertyDescriptor x in TypeDescriptor.GetProperties(memory))
            {
                properties.Add(x.Name, x.GetValue(memory));
            }

            var document = database.CreateDocument();
            //document.PutProperties(Memory.MemoryToDictionary(memory));
            document.PutProperties(properties);

            Console.WriteLine($"Document ID :: {document.Id}");
            Console.WriteLine($"Learning {document.GetProperty("Text")} with location {document.GetProperty("Location")}");

            return new Response<string>
            {
                IsSuccess = true,
                Item = document.Id
            };
        }

        public  Response<List<Memory>> Get()
        {
            var memories = new List<Memory>();
            var memoriesFromDB =  database.CreateAllDocumentsQuery().Run();
            foreach (var memoryRecord in memoriesFromDB)
            {
                memories.Add(Memory.DictToMemory(memoryRecord));
            }

            return new Response<List<Memory>>
            {
                IsSuccess = true,
                Item = memories
            };
        }

        public Response<List<Memory>> Get(SearchParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Response<string> Forget(Memory memory)
        {
            throw new NotImplementedException();
        }
    }
}