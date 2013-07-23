using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBStorage
    [Cmdlet(VerbsCommon.Get, "PBStorage")]
    public class Get_PBStorage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string storageId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.getStorage(storageId));
        }
    }
    #endregion

    #region New_PBStorage
    [Cmdlet(VerbsCommon.New, "PBStorage")]
    public class New_PBStorage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public int size;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string dataCenterId;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string storageName;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string mountImageId;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string profitBricksImagePassword;


        protected override void ProcessRecord()
        {
            createStorageRequest Request = new createStorageRequest();
            Request.size = size;
            Request.dataCenterId = dataCenterId;
            Request.storageName = storageName;
            Request.mountImageId = mountImageId;
            Request.profitBricksImagePassword = profitBricksImagePassword;
            this.WriteObject(PBApiServive.Servive.createStorage(Request));
        }
    }
    #endregion

    #region Add_PBStorageConnection
    [Cmdlet(VerbsCommon.Add, "PBStorageConnection")]
    public class Add_PBStorageConnection : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string storageId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public string serverId;

        // Mandatory=true --> Workarround. busType is enum and so will default 
        // to IDE if if not set on command line
        [Parameter(
            Position = 2,
            Mandatory = true
        )]
        public busType busType;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public int deviceNumber;

        protected override void ProcessRecord()
        {
            connectStorageRequest Request = new connectStorageRequest();
            Request.storageId = storageId;
            Request.serverId = serverId;
            Request.busType = busType;
            // making busType mandatory does require Request.busTypeSpecified = true;
            Request.busTypeSpecified = true;
            Request.deviceNumber = deviceNumber;
            if (deviceNumber != 0)
            {
                Request.deviceNumberSpecified = true;
            }  
            this.WriteObject(PBApiServive.Servive.connectStorageToServer(Request));
        }
    }
    #endregion

    #region Remove_PBStorageConnection
    [Cmdlet(VerbsCommon.Remove, "PBStorageConnection")]
    public class Remove_PBStorageConnection : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]

        public string storageId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public string serverId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.disconnectStorageFromServer(storageId, serverId));
        }
    }
    #endregion

    #region Set_PBStorage
    [Cmdlet(VerbsCommon.Set, "PBStorage")]
    public class Set_PBStorage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string storageId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string storageName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public int size;

        protected override void ProcessRecord()
        {
            updateStorageRequest Request = new updateStorageRequest();
            Request.storageId = storageId;
            Request.storageName = storageName;
            Request.size = size;

            if (size != 0)
            {
                Request.sizeSpecified = true;
            }
            this.WriteObject(PBApiServive.Servive.updateStorage(Request));
        }
    }
    #endregion

    #region Remove_PBStorage
    [Cmdlet(VerbsCommon.Remove, "PBStorage")]
    public class Remove_PBStorage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string storageId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApiServive.Servive.deleteStorage(storageId));
        }
    }
    #endregion
}
