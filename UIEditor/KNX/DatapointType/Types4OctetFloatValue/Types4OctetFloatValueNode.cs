﻿using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAbsoluteTemperature;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAcceleration;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAccelerationAngular;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueActivationEnergy;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueActivity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAmplitude;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngleDeg;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngleRad;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngularFrequency;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngularMomentum;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngularVelocity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueArea;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueCapacitance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueChargeDensitySurface;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueChargeDensityVolume;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueCommonTemperature;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueCompressibility;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueConductance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueDensity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricalConductivity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricCharge;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricCurrent;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricCurrentDensity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricDipoleMoment;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricDisplacement;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricFieldStrength;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricFlux;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricFluxDensity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricPolarization;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricPotential;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricPotentialDifference;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectromagneticMoment;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectromotiveForce;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueEnergy;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueForce;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueFrequency;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueHeatCapacity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueHeatFlowRate;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueHeatQuantity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueImpedance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueLength;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueLightQuantity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueLuminance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueLuminousFlux;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueLuminousIntensity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFieldStrength;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFlux;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticFluxDensity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticMoment;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticPolarization;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagnetization;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagnetomotiveForce;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMass;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMassFlux;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMol;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMomentum;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePhaseAngleDeg;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePhaseAngleRad;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePower;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePowerFactor;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValuePressure;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueReactance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueResistance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueResistivity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSelfInductance;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSolidAngle;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSoundIntensity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSpeed;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueStress;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSurfaceTension;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueTemperatureDifference;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueThermalCapacity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueThermalConductivity;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueThermoelectricPower;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueTime;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueTorque;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueVolume;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueVolumeFlux;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueWeight;
using UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueWork;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue
{
    class Types4OctetFloatValueNode:DatapointType
    {
        public Types4OctetFloatValueNode()
        {
            this.KNXMainNumber = DPT_14;
            this.Name = "4-byte float value";
            this.Type = KNXDataType.Bit32;
        }

        public static TreeNode GetAllTypeNode()
        {
            Types4OctetFloatValueNode nodeType = new Types4OctetFloatValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(ValueAccelerationNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAccelerationAngularNode.GetTypeNode());
            nodeType.Nodes.Add(ValueActivationEnergyNode.GetTypeNode());
            nodeType.Nodes.Add(ValueActivityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMolNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAmplitudeNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAngleRadNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAngleDegNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAngularMomentumNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAngularVelocityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAreaNode.GetTypeNode());
            nodeType.Nodes.Add(ValueCapacitanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueChargeDensitySurfaceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueChargeDensityVolumeNode.GetTypeNode());
            nodeType.Nodes.Add(ValueCompressibilityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueConductanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricalConductivityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueDensityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricChargeNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricCurrentNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricCurrentDensityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricDipoleMomentNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricDisplacementNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricFieldStrengthNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricFluxNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricFluxDensityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricPolarizationNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricPotentialNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectricPotentialDifferenceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectromagneticMomentNode.GetTypeNode());
            nodeType.Nodes.Add(ValueElectromotiveForceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueEnergyNode.GetTypeNode());
            nodeType.Nodes.Add(ValueForceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueFrequencyNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAngularFrequencyNode.GetTypeNode());
            nodeType.Nodes.Add(ValueHeatCapacityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueHeatFlowRateNode.GetTypeNode());
            nodeType.Nodes.Add(ValueHeatQuantityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueImpedanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueLengthNode.GetTypeNode());
            nodeType.Nodes.Add(ValueLightQuantityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueLuminanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueLuminousFluxNode.GetTypeNode());
            nodeType.Nodes.Add(ValueLuminousIntensityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagneticFieldStrengthNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagneticFluxNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagneticFluxDensityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagneticMomentNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagneticPolarizationNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagnetizationNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMagnetomotiveForceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMassNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMassFluxNode.GetTypeNode());
            nodeType.Nodes.Add(ValueMomentumNode.GetTypeNode());
            nodeType.Nodes.Add(ValuePhaseAngleRadNode.GetTypeNode());
            nodeType.Nodes.Add(ValuePhaseAngleDegNode.GetTypeNode());
            nodeType.Nodes.Add(ValuePowerNode.GetTypeNode());
            nodeType.Nodes.Add(ValuePowerFactorNode.GetTypeNode());
            nodeType.Nodes.Add(ValuePressureNode.GetTypeNode());
            nodeType.Nodes.Add(ValueReactanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueResistanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueResistivityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueSelfInductanceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueSolidAngleNode.GetTypeNode());
            nodeType.Nodes.Add(ValueSoundIntensityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueSpeedNode.GetTypeNode());
            nodeType.Nodes.Add(ValueStressNode.GetTypeNode());
            nodeType.Nodes.Add(ValueSurfaceTensionNode.GetTypeNode());
            nodeType.Nodes.Add(ValueCommonTemperatureNode.GetTypeNode());
            nodeType.Nodes.Add(ValueAbsoluteTemperatureNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTemperatureDifferenceNode.GetTypeNode());
            nodeType.Nodes.Add(ValueThermalCapacityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueThermalConductivityNode.GetTypeNode());
            nodeType.Nodes.Add(ValueThermoelectricPowerNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTimeNode.GetTypeNode());
            nodeType.Nodes.Add(ValueTorqueNode.GetTypeNode());
            nodeType.Nodes.Add(ValueVolumeNode.GetTypeNode());
            nodeType.Nodes.Add(ValueVolumeFluxNode.GetTypeNode());
            nodeType.Nodes.Add(ValueWeightNode.GetTypeNode());
            nodeType.Nodes.Add(ValueWorkNode.GetTypeNode());

            return nodeType;
        }
    }
}
