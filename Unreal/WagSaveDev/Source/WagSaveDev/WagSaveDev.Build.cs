// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class WagSaveDev : ModuleRules
{
	public WagSaveDev(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] {
			"Core",
			"CoreUObject",
			"Engine",
			"InputCore",
			"EnhancedInput",
			"AIModule",
			"StateTreeModule",
			"GameplayStateTreeModule",
			"UMG",
			"Slate"
		});

		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.AddRange(new string[] {
			"WagSaveDev",
			"WagSaveDev/Variant_Platforming",
			"WagSaveDev/Variant_Platforming/Animation",
			"WagSaveDev/Variant_Combat",
			"WagSaveDev/Variant_Combat/AI",
			"WagSaveDev/Variant_Combat/Animation",
			"WagSaveDev/Variant_Combat/Gameplay",
			"WagSaveDev/Variant_Combat/Interfaces",
			"WagSaveDev/Variant_Combat/UI",
			"WagSaveDev/Variant_SideScrolling",
			"WagSaveDev/Variant_SideScrolling/AI",
			"WagSaveDev/Variant_SideScrolling/Gameplay",
			"WagSaveDev/Variant_SideScrolling/Interfaces",
			"WagSaveDev/Variant_SideScrolling/UI"
		});

		// Uncomment if you are using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}
