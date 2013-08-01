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
            this.WriteObject(PBApi.Service.getStorage(storageId));
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
            this.WriteObject(PBApi.Service.createStorage(Request));
        }
    }
    #endregion

    #region Connect_PBStorageToServer
    [Cmdlet("Connect", "PBStorageToServer")]
    public class Connect_PBStorageToServer : PBapiPSCmdlet
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
            Mandatory = false
        )]
        public busType? busType;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public int? deviceNumber;

        protected override void ProcessRecord()
        {
            connectStorageRequest Request = new connectStorageRequest();
            Request.storageId = storageId;
            Request.serverId = serverId;
            // If string value spezified is a valid enum
            // set Request.ParemeterSpecified and Parameter

            if (busType.HasValue)
            {
                Request.busType = (busType)busType;
                Request.busTypeSpecified = true;
            }
            if (deviceNumber.HasValue)
            {
                Request.deviceNumber = (int)deviceNumber;
                Request.deviceNumberSpecified = true;
            }

            this.WriteObject(PBApi.Service.connectStorageToServer(Request));
        }
    }
    #endregion

    #region Remove_PBStorageFromServer
    [Cmdlet(VerbsCommon.Remove, "PBStorageFromServer")]
    public class Remove_PBStorageFromServer : PBapiPSCmdlet
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
            this.WriteObject(PBApi.Service.disconnectStorageFromServer(storageId, serverId));
        }
    }
    #endregion

    #region Set_PBStorage
    [Cmdlet(VerbsCommon.Set, "PBStorage")]
    public class Set_PBStorage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
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
        public int? size;

        protected override void ProcessRecord()
        {
            updateStorageRequest Request = new updateStorageRequest();
            if (string.IsNullOrEmpty(storageName) && !size.HasValue)
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: storageName, size"); 
            }
            Request.storageId = storageId;
            Request.storageName = storageName;

            if (size.HasValue)
            {
                Request.size = (int)size;
                Request.sizeSpecified = true;
            }

            this.WriteObject(PBApi.Service.updateStorage(Request));
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
            this.WriteObject(PBApi.Service.deleteStorage(storageId));
        }
    }
    #endregion
}
