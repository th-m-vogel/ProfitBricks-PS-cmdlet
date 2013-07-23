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
            this.WriteObject(PBApi.Servive.getStorage(storageId));
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
            this.WriteObject(PBApi.Servive.createStorage(Request));
        }
    }
    #endregion

    #region Join_PBStorageConnection
    [Cmdlet(VerbsCommon.Join, "PBStorageConnection")]
    public class Join_PBStorageConnection : PBapiPSCmdlet
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

        [Parameter(
            Position = 2,
            Mandatory = true
        )]
        public string busType;

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
            // If string value spezified is a valid enum
            // set Request.ParemeterSpecified and Parameter
            if ((Request.busTypeSpecified = Enum.IsDefined(typeof(busType), busType.ToUpper())))
            {
                Request.busType = (busType)Enum.Parse(typeof(busType), busType.ToUpper());
            }
            Request.deviceNumber = deviceNumber;
            if (deviceNumber != 0)
            {
                Request.deviceNumberSpecified = true;
            }  
            this.WriteObject(PBApi.Servive.connectStorageToServer(Request));
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
            this.WriteObject(PBApi.Servive.disconnectStorageFromServer(storageId, serverId));
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
            this.WriteObject(PBApi.Servive.updateStorage(Request));
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
            this.WriteObject(PBApi.Servive.deleteStorage(storageId));
        }
    }
    #endregion
}
