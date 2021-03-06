// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using StorageTestLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using MS.Test.Common.MsTestLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Management.Storage.ScenarioTest.BVT.HTTP
{
    /// <summary>
    /// Bvt test using name and key context in http mode
    /// </summary>
    [TestClass]
    class NameKeyContextBVT : Management.Storage.ScenarioTest.BVT.HTTPS.NameKeyContextBVT
    {
        [ClassInitialize()]
        public static void NameKeyContextHTTPBVTClassInitialize(TestContext testContext)
        {
            //first set the storage account
            //second init common bvt
            //third set storage context in powershell
            StorageAccountName = Test.Data.Get("StorageAccountName");
            string StorageAccountKey = Test.Data.Get("StorageAccountKey");
            string StorageEndPoint = Test.Data.Get("StorageEndPoint");
            StorageCredentials credential = new StorageCredentials(StorageAccountName, StorageAccountKey);
            useHttps = false;
            isSecondary = false;
            SetUpStorageAccount = Utility.GetStorageAccountWithEndPoint(credential, useHttps, StorageEndPoint);

            CLICommonBVT.CLICommonBVTInitialize(testContext);
            PowerShellAgent.SetStorageContext(StorageAccountName, StorageAccountKey, useHttps, StorageEndPoint);
        }

        [ClassCleanup()]
        public static void NameKeyContextHTTPBVTCleanup()
        {
            CLICommonBVT.CLICommonBVTCleanup();
        }
    }
}
