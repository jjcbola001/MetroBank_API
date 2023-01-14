using Metrobank.Model.Models;
using Microsoft.EntityFrameworkCore;
using Metrobank.DataAccessLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metrobank.DataAccessLibrary.Interfaces
{
    public interface IMetrobankDataAccess
    {
        DbSet<AppMain> AppMains { get; set; }
    }
}
