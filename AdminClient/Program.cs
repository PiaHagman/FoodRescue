using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using System.Threading;
using DataLayer.Backend;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;


AdminBackend admin = new AdminBackend();
admin.CreateAndSeedDb();
Console.WriteLine("Database initialized");
Thread.Sleep(2000);

Console.WriteLine();
