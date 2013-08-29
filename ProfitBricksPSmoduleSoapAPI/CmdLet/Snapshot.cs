using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region New_PBSnapshot
    [Cmdlet(VerbsCommon.New, "PBSnapshot")]
    public class New_PBSnapshot : PBapiPSCmdlet
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
        public string snapshotName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string description;

        protected override void ProcessRecord()
        {
            createSnapshotRequest Request = new createSnapshotRequest();
            Request.storageId = storageId;
            Request.snapshotName = snapshotName;
            Request.description = description;
            this.WriteObject(PBApi.Service.createSnapshot(Request));
        }
    }
    #endregion

    #region Get_PBSnapshots
    [Cmdlet(VerbsCommon.Get, "PBSnapshots")]
    public class Get_PBSnapshots : PBapiPSCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObjects(PBApi.Service.getAllSnapshots());
        }
    }
    #endregion

    #region Get_PBSnapshot
    [Cmdlet(VerbsCommon.Get, "PBSnapshot")]
    public class Get_PBSnapshot : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string snapshotID;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getSnapshot(snapshotID));
        }
    }
    #endregion

    #region Set_PBSnapshot
    [Cmdlet(VerbsCommon.Set, "PBSnapshot")]
    public class Set_PBSnapshot : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string snapshotID;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string description;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string snapshotName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public bool? bootable;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public osType? osType;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public bool? cpuHotPlug;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public bool? ramHotPlug;

        protected override void ProcessRecord()
        {
            updateSnapshotRequest Request = new updateSnapshotRequest();
            if (string.IsNullOrEmpty(snapshotName) && 
                string.IsNullOrEmpty(description) &&
                !bootable.HasValue &&
                !osType.HasValue &&
                !cpuHotPlug.HasValue &&
                !ramHotPlug.HasValue
                )
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: snapshotName, description, bootable, osType, cpuHotPlug, ramHotPlug");
            }
            Request.snapshotId = snapshotID;
            Request.description = description;
            Request.snapshotName = snapshotName;
            if (bootable.HasValue)
            {
                Request.bootable = (bool)bootable;
                Request.bootableSpecified = true;
            }
            if (osType.HasValue)
            {
                Request.osType = (osType)osType;
                Request.osTypeSpecified = true;
            }
            if (cpuHotPlug.HasValue)
            {
                Request.cpuHotPlug = (bool)cpuHotPlug;
                Request.cpuHotPlugSpecified = true;
            }
            if (ramHotPlug.HasValue)
            {
                Request.ramHotPlug = (bool)ramHotPlug;
                Request.ramHotPlugSpecified = true;
            }

            this.WriteObject(PBApi.Service.updateSnapshot(Request));
        }
    }
    #endregion
    
    #region Remove_PBSnapshot
    [Cmdlet(VerbsCommon.Remove, "PBSnapshot")]
    public class Remove_PBSnapshot : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string SnapshotId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteSnapshot(SnapshotId));
        }
    }
    #endregion

    #region Restore_PBSnapshot
    [Cmdlet(VerbsData.Restore, "PBSnapshot")]
    public class Restore_PBSnapshot : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string storageId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public string snapshotId;

        protected override void ProcessRecord()
        {
            rollbackSnapshotRequest Request = new rollbackSnapshotRequest();
            Request.storageId = storageId;
            Request.snapshotId = snapshotId;

            this.WriteObject(PBApi.Service.rollbackSnapshot(Request));
        }
    }
    #endregion
}