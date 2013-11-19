using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region Get_PBImages
    [Cmdlet(VerbsCommon.Get, "PBImages")]
    public class Get_PBImages : PBapiPSCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObjects(PBApi.Service.getAllImages());
        }
    }
    #endregion

    #region Get_PBImage
    [Cmdlet(VerbsCommon.Get, "PBImage")]
    public class Get_PBImage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string imageId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getImage(imageId));
        }
    }
    #endregion

    #region Set_PBImageOStype
    [Cmdlet(VerbsCommon.Set, "PBImageOStype")]
    public class Set_PBImageOStype : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string imageId;

        [Parameter(
            Position = 1,
            Mandatory = true
        )]
        public osType osType;

        protected override void ProcessRecord()
        {
            // prepared for future argument extensions
            //if (
            //    string.IsNullOrEmpty(StringParam) && 
            //    IntParam == 0)
            //{
            //    throw new System.ArgumentException("at leat on of the following parameters must have a valid value: StringParam, IntParam");
            //}
            this.WriteObject(PBApi.Service.setImageOsType(imageId, osType));
        }
    }
    #endregion

    #region Set_PBImage
    [Cmdlet(VerbsCommon.Set, "PBImage")]
    public class Set_PBImage : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string imageUuid;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string name;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public string description;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public bool? bootable;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public ostype? osType;

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

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public bool? nicHotPlug;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public bool? nicHotUnPlug;


        protected override void ProcessRecord()
        {
            updateImageRequest Request = new updateImageRequest();
            if (string.IsNullOrEmpty(name) &&
                string.IsNullOrEmpty(description) &&
                !bootable.HasValue &&
                !osType.HasValue &&
                !cpuHotPlug.HasValue &&
                !ramHotPlug.HasValue &&
                !nicHotPlug.HasValue &&
                !nicHotUnPlug.HasValue
                )
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: snapshotName, description, bootable, osType, cpuHotPlug, ramHotPlug, nicHotPlug, nicHotUnPlug");
            }
            Request.imageUuid = imageUuid;
            Request.description = description;
            Request.name = name;
            if (bootable.HasValue)
            {
                Request.bootable = (bool)bootable;
                Request.bootableSpecified = true;
            }
            if (osType.HasValue)
            {
                Request.osType = (ostype)osType;
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
            if (nicHotPlug.HasValue)
            {
                Request.nicHotPlug = (bool)nicHotPlug;
                Request.nicHotPlugSpecified = true;
            }
            if (nicHotUnPlug.HasValue)
            {
                Request.nicHotUnPlug = (bool)nicHotUnPlug;
                Request.nicHotUnPlugSpecified = true;
            }

            this.WriteObject(PBApi.Service.updateImage(Request));
        }
    }
    #endregion

}
