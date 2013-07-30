using ProfitBricksPSmoduleSoapAPI.WsProfitBricksApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProfitBricksPSmoduleSoapAPI.CmdLet
{
    #region New_PBFirewallRules
    [Cmdlet(VerbsCommon.New, "PBFirewallRules")]
    public class New_PBFirewallRules : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string nicId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public firewallRuleRequest[] firewallRuleRequest;

        protected override void ProcessRecord()
        {
            // not implemented yet
            // PBapiChecks.IsFWrule(request)
            this.WriteObject(PBApi.Service.addFirewallRulesToNic(firewallRuleRequest, nicId));
        }
    }
    #endregion

    #region New_PBFirewallRule
    [Cmdlet(VerbsCommon.New, "PBFirewallRule")]
    public class New_PBFirewallRule : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string nicId;

        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string firewallRuleName;

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public protocol protocol;

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public string sourceMac;

        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public string sourceIp;

        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public string targetIp;

        [Parameter(
            Position = 6,
            Mandatory = false
        )]
        public int portRangeStart;

        [Parameter(
            Position = 7,
            Mandatory = false
        )]
        public int portRangeEnd;

        [Parameter(
            Position = 8,
            Mandatory = false
        )]
        public string icmpType;

        [Parameter(
            Position = 9,
            Mandatory = false
        )]
        public string icmpCode;

        protected override void ProcessRecord()
        {
            firewallRuleRequest[] request = { new firewallRuleRequest() }; 
            if (
                string.IsNullOrWhiteSpace(sourceMac) &&
                string.IsNullOrWhiteSpace(sourceIp) &&
                string.IsNullOrWhiteSpace(targetIp) &&
                portRangeStart == 0 &&
                portRangeEnd == 0 &&
                string.IsNullOrWhiteSpace(icmpType) &&
                string.IsNullOrWhiteSpace(icmpCode)
                )
            {
                throw new System.ArgumentException("at leat on of the following parameters must have a valid value: sourceMac, sourceIp, targetIp, portRangeStart, portRangeEnd, icmpType, icmpCode");
            }
            if (!string.IsNullOrWhiteSpace(sourceMac))
            {
                PBapiChecks.IsMAC(sourceMac);
            }
            if (!string.IsNullOrWhiteSpace(sourceIp))
            {
                PBapiChecks.IsIP(sourceIp);
            }
            if (!string.IsNullOrWhiteSpace(targetIp))
            {
                PBapiChecks.IsIP(targetIp);
            }
            if ((portRangeStart > 65534) || (portRangeEnd > 65534))
            {
                throw new System.ArgumentException("maximum allowed value for portRangeStart and portRangeEnd is 65534");
            }
            if (!string.IsNullOrWhiteSpace(icmpCode)) 
            {
                if (Convert.ToInt32(icmpCode) > 255)
                {
                    throw new System.ArgumentException("maximum allowed value for icmpCode is 255");
                }    
                request[0].icmpCode = Convert.ToInt32(icmpCode);
                request[0].icmpCodeSpecified = true;
            }
            if (!string.IsNullOrWhiteSpace(icmpType))
            {
                if (Convert.ToInt32(icmpType) > 255)
                {
                    throw new System.ArgumentException("maximum allowed value for icmpType is 255");
                }    
                request[0].icmpType = Convert.ToInt32(icmpType); 
                request[0].icmpTypeSpecified = true;
             }
            if ((portRangeEnd > 0) || (portRangeStart > 0))
            {
                if (protocol != protocol.TCP || protocol != protocol.UDP)
                {
                    throw new System.ArgumentException("if portRangeEnd(Start) is specified, protocoll must be TCP or UDP");
                }
                if ((portRangeEnd == 0) || (portRangeStart == 0))
                {
                    throw new System.ArgumentException("if portRangeEnd(start) is specified, portRangeStart(end) must have a value from 1 to 65534 also");
                }
                if (portRangeEnd < portRangeStart)
                {
                    throw new System.ArgumentException("portRangeEnd less than portRangeStart is not allowed");
                }
                if (protocol == protocol.TCP)
                request[0].portRangeEnd = portRangeEnd;
                request[0].portRangeEndSpecified = true;
                request[0].portRangeStart = portRangeStart;
                request[0].portRangeStartSpecified = true;
            }
            request[0].sourceIp = sourceIp;
            request[0].targetIp = targetIp;
            request[0].sourceMac = sourceMac;
            request[0].protocol = protocol;
            // not implemented yet in wsdl
            // request[0].firewallRuleName = firewallRuleName;
            
            // not implemented yet
            // PBapiChecks.IsFWrule(request)
            this.WriteObject(PBApi.Service.addFirewallRulesToNic(request, nicId));
        }
    }
    #endregion

    #region Get_PBFirewall
    [Cmdlet(VerbsCommon.Get, "PBFirewall")]
    public class Get_PBFirewall : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string firewallId;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.getFirewall(firewallId));
        }
    }
    #endregion

    #region Remove_PBFirewallRules
    [Cmdlet(VerbsCommon.Remove, "PBFirewallRules")]
    public class Remove_PBFirewallRules : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallRuleIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.removeFirewallRules(firewallRuleIds));
        }
    }
    #endregion

    #region Switch_PBFirewallStatus
    [Cmdlet(VerbsCommon.Switch, "PBFirewallStatus")]
    public class Switch_PBFirewallStatus : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.activateFirewalls(firewallIds));
        }
    }
    #endregion

    #region Remove_PBFirewalls
    [Cmdlet(VerbsCommon.Switch, "PBFirewalls")]
    public class Remove_PBFirewalls : PBapiPSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true
        )]
        public string[] firewallIds;

        protected override void ProcessRecord()
        {
            this.WriteObject(PBApi.Service.deleteFirewalls(firewallIds));
        }
    }
    #endregion


}
