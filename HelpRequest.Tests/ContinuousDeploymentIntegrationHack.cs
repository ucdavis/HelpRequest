﻿using System;

namespace HelpRequest.Tests
{
    public class ContinuousIntegrationDeploymentHack
    {
        public ContinuousIntegrationDeploymentHack()
        {
            //new log4net.Appender.AdoNetAppender();
            new NHibernate.ByteCode.Spring.ProxyFactoryFactory();
            new System.Data.SQLite.SQLiteException();

            throw new Exception("This class should never be called or instantiated");
        }
    }
}