
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypesN8.ActuatorConnectType;
using KNX.DatapointType.TypesN8.ADAType;
using KNX.DatapointType.TypesN8.AddInfoTypes;
using KNX.DatapointType.TypesN8.AlarmClassType;
using KNX.DatapointType.TypesN8.ApplicationArea;
using KNX.DatapointType.TypesN8.BackupMode;
using KNX.DatapointType.TypesN8.BeaufortWindForceScale;
using KNX.DatapointType.TypesN8.BehaviourBusPowerUpDown;
using KNX.DatapointType.TypesN8.BehaviourLockUnlock;
using KNX.DatapointType.TypesN8.BlindsControlMode;
using KNX.DatapointType.TypesN8.BlinkingMode;
using KNX.DatapointType.TypesN8.BuildingMode;
using KNX.DatapointType.TypesN8.BurnerType;
using KNX.DatapointType.TypesN8.ChangeoverMode;
using KNX.DatapointType.TypesN8.CommMode;
using KNX.DatapointType.TypesN8.DALIFadeTime;
using KNX.DatapointType.TypesN8.DamperMode;
using KNX.DatapointType.TypesN8.DHWMode;
using KNX.DatapointType.TypesN8.DimmPBModel;
using KNX.DatapointType.TypesN8.ErrorClassHVAC;
using KNX.DatapointType.TypesN8.ErrorClassSystem;
using KNX.DatapointType.TypesN8.FanMode;
using KNX.DatapointType.TypesN8.FuelType;
using KNX.DatapointType.TypesN8.HeaterMode;
using KNX.DatapointType.TypesN8.HVACContrMode;
using KNX.DatapointType.TypesN8.HVACEmergMode;
using KNX.DatapointType.TypesN8.HVACMode;
using KNX.DatapointType.TypesN8.LightApplicationMode;
using KNX.DatapointType.TypesN8.LightControlMode;
using KNX.DatapointType.TypesN8.LoadPriority;
using KNX.DatapointType.TypesN8.LoadTypeDetected;
using KNX.DatapointType.TypesN8.LoadTypeSet;
using KNX.DatapointType.TypesN8.MasterSlaveMode;
using KNX.DatapointType.TypesN8.MeteringDeviceType;
using KNX.DatapointType.TypesN8.OccMode;
using KNX.DatapointType.TypesN8.PBAction;
using KNX.DatapointType.TypesN8.Priority;
using KNX.DatapointType.TypesN8.PSUMode;
using KNX.DatapointType.TypesN8.RFFilterSelect;
using KNX.DatapointType.TypesN8.RFModeSelect;
using KNX.DatapointType.TypesN8.SABBehaviourLockUnlock;
using KNX.DatapointType.TypesN8.SABExceptBehaviour;
using KNX.DatapointType.TypesN8.SCLOMode;
using KNX.DatapointType.TypesN8.SensorSelect;
using KNX.DatapointType.TypesN8.SSSBMode;
using KNX.DatapointType.TypesN8.StartSynchronization;
using KNX.DatapointType.TypesN8.StatusRoomSetp;
using KNX.DatapointType.TypesN8.SwitchOnMode;
using KNX.DatapointType.TypesN8.SwitchPBModel;
using KNX.DatapointType.TypesN8.TimeDelay;
using KNX.DatapointType.TypesN8.ValveMode;

namespace KNX.DatapointType.TypesN8
{
    class TypesN8Node:DatapointType
    {
        public TypesN8Node()
        {
            this.KNXMainNumber = DPT_20;
            this.DPTName = "1-byte";
            this.Type = KNXDataType.Bit8;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypesN8Node nodeType = new TypesN8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(SCLOModeNode.GetTypeNode());
            nodeType.Nodes.Add(BuildingModeNode.GetTypeNode());
            nodeType.Nodes.Add(OccModeNode.GetTypeNode());
            nodeType.Nodes.Add(PriorityNode.GetTypeNode());
            nodeType.Nodes.Add(LightApplicationModeNode.GetTypeNode());
            nodeType.Nodes.Add(ApplicationAreaNode.GetTypeNode());
            nodeType.Nodes.Add(AlarmClassTypeNode.GetTypeNode());
            nodeType.Nodes.Add(PSUModeNode.GetTypeNode());
            nodeType.Nodes.Add(ErrorClassSystemNode.GetTypeNode());
            nodeType.Nodes.Add(ErrorClassHVACNode.GetTypeNode());
            nodeType.Nodes.Add(TimeDelayNode.GetTypeNode());
            nodeType.Nodes.Add(BeaufortWindForceScaleNode.GetTypeNode());
            nodeType.Nodes.Add(SensorSelectNode.GetTypeNode());
            nodeType.Nodes.Add(ActuatorConnectTypeNode.GetTypeNode());
            nodeType.Nodes.Add(FuelTypeNode.GetTypeNode());
            nodeType.Nodes.Add(BurnerTypeNode.GetTypeNode());
            nodeType.Nodes.Add(HVACModeNode.GetTypeNode());
            nodeType.Nodes.Add(DHWModeNode.GetTypeNode());
            nodeType.Nodes.Add(LoadPriorityNode.GetTypeNode());
            nodeType.Nodes.Add(HVACContrModeNode.GetTypeNode());
            nodeType.Nodes.Add(HVACEmergModeNode.GetTypeNode());
            nodeType.Nodes.Add(ChangeoverModeNode.GetTypeNode());
            nodeType.Nodes.Add(ValveModeNode.GetTypeNode());
            nodeType.Nodes.Add(DamperModeNode.GetTypeNode());
            nodeType.Nodes.Add(HeaterModeNode.GetTypeNode());
            nodeType.Nodes.Add(FanModeNode.GetTypeNode());
            nodeType.Nodes.Add(MasterSlaveModeNode.GetTypeNode());
            nodeType.Nodes.Add(StatusRoomSetpNode.GetTypeNode());
            nodeType.Nodes.Add(MeteringDeviceTypeNode.GetTypeNode());
            nodeType.Nodes.Add(ADATypeNode.GetTypeNode());
            nodeType.Nodes.Add(BackupModeNode.GetTypeNode());
            nodeType.Nodes.Add(StartSynchronizationNode.GetTypeNode());
            nodeType.Nodes.Add(BehaviourLockUnlockNode.GetTypeNode());
            nodeType.Nodes.Add(BehaviourBusPowerUpDownNode.GetTypeNode());
            nodeType.Nodes.Add(DALIFadeTimeNode.GetTypeNode());
            nodeType.Nodes.Add(BlinkingModeNode.GetTypeNode());
            nodeType.Nodes.Add(LightControlModeNode.GetTypeNode());
            nodeType.Nodes.Add(SwitchPBModelNode.GetTypeNode());
            nodeType.Nodes.Add(PBActionNode.GetTypeNode());
            nodeType.Nodes.Add(DimmPBModelNode.GetTypeNode());
            nodeType.Nodes.Add(SwitchOnModeNode.GetTypeNode());
            nodeType.Nodes.Add(LoadTypeSetNode.GetTypeNode());
            nodeType.Nodes.Add(LoadTypeDetectedNode.GetTypeNode());
            nodeType.Nodes.Add(SABExceptBehaviourNode.GetTypeNode());
            nodeType.Nodes.Add(SABBehaviourLockUnlockNode.GetTypeNode());
            nodeType.Nodes.Add(SSSBModeNode.GetTypeNode());
            nodeType.Nodes.Add(BlindsControlModeNode.GetTypeNode());
            nodeType.Nodes.Add(CommModeNode.GetTypeNode());
            nodeType.Nodes.Add(AddInfoTypesNode.GetTypeNode());
            nodeType.Nodes.Add(RFModeSelectNode.GetTypeNode());
            nodeType.Nodes.Add(RFFilterSelectNode.GetTypeNode());

            return nodeType;
        }
    }
}
