﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doancuoiky.Model
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
