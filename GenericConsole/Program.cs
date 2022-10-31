// See https://aka.ms/new-console-template for more information

using GenericClass.Lib;

Export<Person> ex = new Export<Person>();

List<Person> people = ex.ImportCSV(Environment.CurrentDirectory,@"test.csv" , ',');
ex.ExportCSV(people, Environment.CurrentDirectory, @"ganzlustig.csv");