using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CalamityMod.Balancing;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.DataStructures;
using CalamityMod.Enums;
using CalamityMod.Events;
using CalamityMod.FluidSimulation;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.Fishing;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurniturePlagued;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.DraedonLabThings;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using CalamityMod.Systems.Collections;
using CalamityMod.Systems.Mechanic;
using CalamityMod.Tiles;
using CalamityMod.Tiles.DraedonStructures;
using CalamityMod.Tiles.FurnitureExo;
using CalamityMod.Walls;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Graphics.CameraModifiers;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Light;
using Terraria.Graphics.Renderers;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.UI.Gamepad;
using Terraria.WorldBuilding;

namespace CalamityMod.ILEditing;

public class ILChanges : ILoadable
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static Action<ManipulatorContext> _003C0_003E__ApplyEdits;

		public static Manipulator _003C1_003E__CustomDoDrawChanges;

		public static hook_DrawCursor _003C2_003E__UseCoolFireCursorEffect;

		public static hook_SortDrawCacheWorms _003C3_003E__DrawFusableParticles;

		public static hook_Draw _003C4_003E__ClearTilePings;

		public static hook_ModifyItemDropFromNPC _003C5_003E__ColorBlightedGel;

		public static hook_RequestLight _003C6_003E__DisableFlashesWithPhotosensitivityConfig;

		public static Manipulator _003C7_003E__DisableCullingForTreeAndCactus;

		public static Manipulator _003C8_003E__DrawTreeGlowMask;

		public static hook_DrawBasicTile _003C9_003E__DrawTreeTrunkAndCactusGlowMask;

		public static Manipulator _003C10_003E__PermitNighttimeTownNPCSpawning;

		public static hook_UpdateTime_SpawnTownNPCs _003C11_003E__AlterTownNPCSpawnRate;

		public static Manipulator _003C12_003E__PreventVanillaBossDeathsInBossRush;

		public static hook_NPCLoot _003C13_003E__PreventDiabolistLootLogic;

		public static Manipulator _003C14_003E__MakeTaxCollectorUseful;

		public static hook_ApplyTileCollision _003C15_003E__AllowFusionFeederToDigThroughSand;

		public static Manipulator _003C16_003E__ScopesRequireVisibilityToZoom;

		public static Manipulator _003C17_003E__DodgeMechanicAdjustments;

		public static hook_PutHallowedArmorSetBonusOnCooldown _003C18_003E__AddHolyProtectionCooldown;

		public static Manipulator _003C19_003E__FixAllDashMechanics;

		public static hook_DashMovement _003C20_003E__DashMovementEdits;

		public static hook_DoCommonDashHandle _003C21_003E__ApplyDashKeybind;

		public static hook_KeyDoubleTap _003C22_003E__DisableDoubleTapOnConfig;

		public static Manipulator _003C23_003E__MakeShieldSlamIFramesConsistent;

		public static Manipulator _003C24_003E__NerfShieldOfCthulhuBonkSafety;

		public static hook_OpenDoor _003C25_003E__OpenDoor_LabDoorOverride;

		public static hook_CloseDoor _003C26_003E__CloseDoor_LabDoorOverride;

		public static hook_AffixName _003C27_003E__IncorporateEnchantmentInAffix;

		public static hook_NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float_float _003C28_003E__IncorporateExtraProjectileVariables;

		public static hook_ApplyDamageToNPC _003C29_003E__ApplyOldFashionedDamageToMiscHits;

		public static Manipulator _003C30_003E__AddTwinklersToStatue;

		public static hook_GetDifficulty _003C31_003E__GetDifficultyOverride;

		public static hook_GetShimmered _003C32_003E__ShimmerEffectEdits;

		public static hook_Teleport _003C33_003E__TPOverride;

		public static hook_DrawSingleTile _003C34_003E__GlowMaskTileRender;

		public static hook_PlaceThing_CannonBall _003C35_003E__AllowCannonJellyfishUse;

		public static hook_IsItemSlotUnlockedAndUsable _003C36_003E__MasterModeCelestialOnionCheck;

		public static hook_AI_007_GrapplingHooks _003C37_003E__AllowHooksToGrabArenabox;

		public static hook_SolidCollision_Vector2_int_int _003C38_003E__ArenaCollision_Vector2_int_int;

		public static hook_SolidCollision_Vector2_int_int_bool _003C39_003E__ArenaCollision_Vector2_int_int_bool;

		public static hook_TileCollision _003C40_003E__ArenaCollision_TileCollision;

		public static Manipulator _003C41_003E__ChaliceBufferHeal;

		public static hook_CheckMana_int_bool_bool _003C42_003E__AllowNegativeCheckMana;

		public static hook_CheckMana_Item_int_bool_bool _003C43_003E__AllowNegativeCheckMana;

		public static hook_GrappleMovement _003C44_003E__CustomGrappleMovementCheck;

		public static hook_UpdatePettingAnimal _003C45_003E__CustomGrapplePreDefaultMovement;

		public static hook_PlayerFrame _003C46_003E__CustomGrapplePostFrame;

		public static hook_SlopeDownMovement _003C47_003E__CustomGrapplePreStepUp;

		public static hook_DamageVar_float_int_float _003C48_003E__AdjustDamageVariance;

		public static Manipulator _003C49_003E__RemoveExpertHardmodeScaling;

		public static Manipulator _003C50_003E__VanillaBossResistChanges;

		public static Manipulator _003C51_003E__LimitTerrarianProjectiles;

		public static Manipulator _003C52_003E__StardustGuardianAttackBuffs;

		public static hook_ConsumeSolarFlare _003C53_003E__SolarWingsDashChange;

		public static hook_IsDamageDodgable _003C54_003E__GFBNurseMeteorUndodgeable;

		public static Manipulator _003C55_003E__UpdateBuffsBalancingChanges;

		public static Manipulator _003C56_003E__RemoveBeetleAndSolarFlareMultiplicativeDR;

		public static Manipulator _003C57_003E__FixJumpHeightBoosts;

		public static Manipulator _003C58_003E__BaseJumpSpeedAdjustment;

		public static Manipulator _003C59_003E__RunSpeedAdjustments;

		public static Manipulator _003C60_003E__NerfOverpoweredRunAccelerationSources;

		public static Manipulator _003C61_003E__RemoveSoaringInsigniaInfiniteWingTime;

		public static Manipulator _003C62_003E__UpdateLifeRegenBalancingChanges;

		public static Manipulator _003C63_003E__UpdateManaRegenBalancingChanges;

		public static Manipulator _003C64_003E__ManaRegenDelayAdjustment;

		public static Manipulator _003C65_003E__RemoveFrozenInflictionFromDeerclopsIceSpikes;

		public static Manipulator _003C66_003E__BlockLivingTreesNearOcean;

		public static hook_SmashAltar _003C67_003E__PreventSmashAltarCode;

		public static Manipulator _003C68_003E__AdjustChlorophyteSpawnRate;

		public static Manipulator _003C69_003E__AdjustChlorophyteSpawnLimits;

		public static Manipulator _003C70_003E__ChangeDefaultWorldSize;

		public static Manipulator _003C71_003E__SwapSmallDescriptionKey;

		public static hook_MakeDungeon _003C72_003E__LimitDungeonEntranceXPosition;

		public static Manipulator _003C73_003E__LimitDungeonHallsXPosition;

		public static Manipulator _003C74_003E__RemoveExpertBrainRandomDebuffs;

		public static hook_HitEffect_HitInfo _003C75_003E__PreventLavaSlimeLavaDrop;

		public static Manipulator _003C76_003E__LetDetonatingBubblesTakeDamage;

		public static Manipulator _003C77_003E__PunchCameraUsesScreenshakeConfig;

		public static Manipulator _003C78_003E__MakeMagmaStoneFireGauntletDustToggleable;

		public static Manipulator _003C79_003E__MakeMagmaStoneFireGauntletProjectileDustToggleable;

		public static Manipulator _003C80_003E__DecreaseSandstormWindSpeedRequirement;

		public static Manipulator _003C81_003E__RelaxPrefixRequirements;

		public static hook_SlimeRainSpawns _003C82_003E__PreventBossSlimeRainSpawns;

		public static hook_IsItemTransformLocked _003C83_003E__AdjustShimmerRequirements;

		public static hook_AI_015_Flails _003C84_003E__FlailsNoLongerAffectedByPlayerVelocity;

		public static Manipulator _003C85_003E__MakeMeteoriteExplodable;

		public static Manipulator _003C86_003E__BloodMoonsRequire200MaxLife;

		public static Manipulator _003C87_003E__PreventFossilShattering;

		public static hook_GetPickaxeDamage _003C88_003E__RemoveHellforgePickaxeRequirement;

		public static Manipulator _003C89_003E__PreventUFODismountInWater;

		public static hook_GetAnglerReward_MainReward _003C90_003E__AddMoreGuaranteedAnglerRewards;

		public static hook_GetAnglerReward_Bait _003C91_003E__ImproveAnglerBaitReward;

		public static hook_GetAnglerReward_Money _003C92_003E__ImproveAnglerMoneyReward;

		public static Manipulator _003C93_003E__RemovePowerCellPlanteraLock;

		public static hook_ItemCheck_CheckCanUse _003C94_003E__RemoveUseLocks;

		public static hook_ItemCheck_UseEventItems _003C95_003E__ApplyCelestialSigilChanges;

		public static Manipulator _003C96_003E__RemoveDamageConditionFromRadar;

		public static hook_UpdateControlHolds _003C97_003E__DelayGravity;

		public static hook_SetZoom_MouseInWorld _003C98_003E__GravityMouse;

		public static hook_DrawPlayerChatBubbles _003C99_003E__UI_Unflip_Start;

		public static hook_DrawInterface _003C100_003E__UI_Unflip_End;

		public static hook_DrawHeldProj _003C101_003E__FixHeldProjectileBlendState;

		public static Manipulator _003C102_003E__FixTruffleWormFishing;

		public static Manipulator _003C103_003E__UseVisibleThroughWaterMapTile;

		public static Manipulator _003C104_003E__EnsureCheckDeadOnSegments;
	}

	private const float VanillaBaseJumpSpeed = 5.01f;

	private static int labDoorOpen = -1;

	private static int labDoorClosed = -1;

	private static int aLabDoorOpen = -1;

	private static int aLabDoorClosed = -1;

	private static int exoDoorOpen = -1;

	private static int exoDoorClosed = -1;

	private static readonly Func<Player, int> CalamityDashEquipped = (Player p) => p.Calamity().HasCustomDash ? 1 : 0;

	private static bool HasLoggedHeldProjectileBlendStateCatch = false;

	private static readonly Func<Player, bool> MagmaStoneVisualsEnabled = (Player p) => p.Calamity().magmaStoneVisuals;

	[Obsolete("Use 'Main.instance.TilesRenderer.Wind' Instead. This property is included in the Calamity source code only for historic value.", true)]
	public static WindGrid Windgrid { get; internal set; }

	public static int DungeonHallXLimit => DungeonHallXLimitOverride ?? (SulphurousSea.BiomeWidth + 25);

	public static int DungeonBaseXLimit => DungeonBaseXLimitOverride ?? (SulphurousSea.BiomeWidth + 167);

	public static int? DungeonHallXLimitOverride { get; set; }

	public static int? DungeonBaseXLimitOverride { get; set; }

	private static bool AdjustShimmerRequirements(orig_IsItemTransformLocked orig, int type)
	{
		if (type == 1326)
		{
			if (DownedBossSystem.downedCalamitas)
			{
				return !DownedBossSystem.downedExoMechs;
			}
			return true;
		}
		return orig.Invoke(type);
	}

	private static void RemoveSoaringInsigniaInfiniteWingTime(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "empressBrooch")
		}))
		{
			LogFailure("Soaring Insignia Infinite Flight Removal", "Could not locate the Soaring Insignia bool.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.And);
	}

	private static void FixJumpHeightBoosts(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 20)
		}))
		{
			LogFailure("Jump Height Boost Fixes", "Could not locate Shiny Red Balloon jump height assignment value.");
			return;
		}
		cursor.RemoveRange(2);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 6.51f)
		}))
		{
			LogFailure("Jump Height Boost Fixes", "Could not locate Shiny Red Balloon jump speed assignment value.");
			return;
		}
		cursor.Prev.Operand = 0.75f;
		cursor.Emit(OpCodes.Ldsfld, typeof(Player).GetField("jumpSpeed"));
		cursor.Emit(OpCodes.Add);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 1.8f)
		}))
		{
			LogFailure("Jump Height Boost Fixes", "Could not locate Soaring Insignia jump speed boost value.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 0.5f);
	}

	private static void BaseJumpSpeedAdjustment(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 5.01f)
		}))
		{
			LogFailure("Base Jump Height Buff", "Could not locate the jump height variable.");
			return;
		}
		cursor.Remove();
		cursor.EmitDelegate<Func<float>>((Func<float>)(() => (!CalamityServerConfig.Instance.FasterJumpSpeed) ? 5.01f : BalancingConstants.ConfigBoostedBaseJumpSpeed));
	}

	private static void RunSpeedAdjustments(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		float asphaltTopSpeedMultiplier = 2.25f;
		float asphaltSlowdown = 1f;
		float iceSkateAcceleration = 2.1f;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 3.5f)
		}))
		{
			LogFailure("Run Speed Adjustments", "Could not locate Asphalt's top speed multiplier.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, asphaltTopSpeedMultiplier);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 2f)
		}))
		{
			LogFailure("Run Speed Adjustments", "Could not locate Asphalt's run slowdown multiplier.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, asphaltSlowdown);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 3.5f)
		}))
		{
			LogFailure("Run Speed Adjustments", "Could not locate Ice Skates + Frozen Slime Block acceleration multiplier.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, iceSkateAcceleration);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 3.5f)
		}))
		{
			LogFailure("Run Speed Adjustments", "Could not locate Ice Skates + Ice Block acceleration multiplier.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, iceSkateAcceleration);
	}

	private static void NerfOverpoweredRunAccelerationSources(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "empressBrooch")
		}))
		{
			LogFailure("Run Acceleration Nerfs", "Could not locate the Soaring Insignia bool.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 1.75f)
		}))
		{
			LogFailure("Run Acceleration Nerfs", "Could not locate the Soaring Insignia run acceleration multiplier.");
			return;
		}
		cursor.Next.Operand = 1.25f;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "shadowArmor")
		}))
		{
			LogFailure("Run Acceleration Nerfs", "Could not locate the Shadow Armor bool.");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.EmitDelegate<Func<bool, Player, int>>((Func<bool, Player, int>)delegate(bool shadowArmor, Player p)
		{
			if (!shadowArmor)
			{
				return 0;
			}
			if (p.hasMagiluminescence && p.velocity.Y == 0f)
			{
				return 0;
			}
			p.runAcceleration *= BalancingConstants.ShadowArmorRunAccelerationMultiplier;
			p.maxRunSpeed *= BalancingConstants.ShadowArmorMaxRunSpeedMultiplier;
			p.accRunSpeed *= BalancingConstants.ShadowArmorAccRunSpeedMultiplier;
			p.runSlowdown *= BalancingConstants.ShadowArmorRunSlowdownMultiplier;
			return 0;
		});
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "empressBrooch")
		}))
		{
			LogFailure("Run Acceleration Nerfs", "Could not locate the Soaring Insignia bool.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.And);
	}

	private static void UpdateLifeRegenBalancingChanges(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "nebulaLevelLife")
		}))
		{
			LogFailure("Nebula Armor DoT Ignoring Nerf", "Could not locate the Nebula Armor Life Booster variable.");
			return;
		}
		cursor.Emit(OpCodes.Pop);
		cursor.Emit(OpCodes.Ldc_I4_0);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "wellFed")
		}))
		{
			LogFailure("Expert Mode Well Fed Reduced Life Regen Prevention", "Could not locate the Well Fed bool.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_1);
		cursor.Emit(OpCodes.Or);
	}

	private static void ManaRegenDelayAdjustment(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 45f)
		}))
		{
			LogFailure("Max Mana Regen Delay Reduction", "Could not locate the max mana regen flat variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 20f);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0.7f)
		}))
		{
			LogFailure("Max Mana Regen Delay Reduction", "Could not locate the max mana regen delay multiplier variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 0.2f);
	}

	private static void UpdateManaRegenBalancingChanges(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 6)
		}))
		{
			LogFailure("Nebula Armor Mana Regen Nerf", "Could not locate the Nebula Armor mana regeneration frame counter threshold.");
			return;
		}
		cursor.Next.Operand = BalancingConstants.NebulaManaRegenFrameCounterThreshold;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0.8f)
		}))
		{
			LogFailure("Mana Regen Buff", "Could not locate the mana regen multiplier variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 0.25f);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0.2f)
		}))
		{
			LogFailure("Mana Regen Buff", "Could not locate the flat mana regen variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 0.75f);
	}

	private static int AdjustDamageVariance(orig_DamageVar_float_int_float orig, float dmg, int percent, float luck)
	{
		if (percent == Main.DefaultDamageVariationPercent)
		{
			percent = 5;
		}
		return orig.Invoke(dmg, percent, 0f);
	}

	private static void RemoveExpertHardmodeScaling(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 1000)
		}))
		{
			LogFailure("Expert Hardmode Scaling Removal", "Could not locate the HP check.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4_M1);
	}

	private static void VanillaBossResistChanges(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		for (int f = 0; f < 2; f++)
		{
			if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
			{
				(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 1002)
			}))
			{
				LogFailure("Reduce EoW Grenade Resist", "Could not move to the resist factor.");
				return;
			}
		}
		if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 5f)
		}))
		{
			LogFailure("Reduce EoW Grenade Resist", "Could not move to the resist factor.");
			return;
		}
		cursor.EmitPop();
		cursor.EmitLdcR4(3f);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdsfld(i, typeof(ProjectileID.Sets), "CultistIsResistantTo")
		}))
		{
			LogFailure("Lunatic Cultist Homing Resist Removal", "Could not locate the Cultist resist set.");
		}
		else if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0.75f)
		}))
		{
			LogFailure("Lunatic Cultist Homing Resist Removal", "Could not locate the resist percentage.");
		}
		else
		{
			cursor.Next.Operand = 1f;
		}
	}

	private static void LimitTerrarianProjectiles(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 603)
		}))
		{
			LogFailure("Limit Terrarian Yoyo Projectiles", "Could not locate the yoyo ID.");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.EmitDelegate<Func<int, Projectile, int>>((Func<int, Projectile, int>)((int x, Projectile p) => (!p.FinalExtraUpdate()) ? int.MinValue : x));
	}

	private static void UpdateBuffsBalancingChanges(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcI4(val, 98)
		}))
		{
			LogFailure("Beetle Scale Mail Nerf", "Could not locate the Beetle Might buff ID.");
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
			{
				(Instruction val) => ILPatternMatchingExt.MatchLdcR4(val, 0.1f)
			}))
			{
				LogFailure("Beetle Scale Mail Nerf", "Could not locate the amount of melee speed granted.");
				return;
			}
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, BalancingConstants.BeetleScaleMailMeleeSpeedPerBeetle);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcI4(val, 173)
		}))
		{
			LogFailure("Nebula Armor Nerf", "Could not locate the Nebula Life buff ID.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdfld<Player>(val, "lifeRegen")
		}))
		{
			LogFailure("Nebula Armor Nerf", "Could not locate the player's life regen being loaded.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcI4(val, 6)
		}))
		{
			LogFailure("Nebula Armor Nerf", "Could not locate the amount of life regen to grant.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, BalancingConstants.NebulaLifeRegenPerBooster);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcI4(val, 179)
		}))
		{
			LogFailure("Nebula Armor Nerf", "Could not locate the Nebula Damage buff ID.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcR4(val, 0.15f)
		}))
		{
			LogFailure("Nebula Armor Nerf", "Could not locate the amount of damage to grant.");
			return;
		}
		cursor.Next.Operand = BalancingConstants.NebulaDamagePerBooster;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcR4(val, 0.01f)
		}))
		{
			LogFailure("Remove Feral Bite Random Debuffs", "Could not locate the Feral Bite random debuff duration multiplier.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 0f);
	}

	private static void DashMovementEdits(orig_DashMovement orig, Player self)
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		if (self.dash == 2 && self.eocDash > 0 && self.eocHit < 0)
		{
			Rectangle DashHitbox = default(Rectangle);
			((Rectangle)(ref DashHitbox))._002Ector((int)((double)self.position.X + (double)self.velocity.X * 0.5 - 4.0), (int)((double)self.position.Y + (double)self.velocity.Y * 0.5 - 4.0), self.width + 8, self.height + 8);
			for (int i = 0; i < 200; i++)
			{
				NPC hitNPC = Main.npc[i];
				if (!hitNPC.active || hitNPC.dontTakeDamage || hitNPC.friendly || (hitNPC.aiStyle == 112 && !(hitNPC.ai[2] <= 1f)) || !self.CanNPCBeHitByPlayerOrPlayerProjectile(hitNPC))
				{
					continue;
				}
				Rectangle npcHitbox = hitNPC.getRect();
				if (((Rectangle)(ref DashHitbox)).Intersects(npcHitbox) && (hitNPC.noTileCollide || self.CanHit(hitNPC)))
				{
					float dmg = self.GetTotalDamage(DamageClass.Melee).ApplyTo(self.Calamity().copyrightInfringementShield ? 300f : 30f);
					float kb = self.GetTotalKnockback(DamageClass.Melee).ApplyTo(self.Calamity().copyrightInfringementShield ? 12f : 9f);
					bool crit = false;
					if ((float)Main.rand.Next(100) < self.GetTotalCritChance(DamageClass.Melee))
					{
						crit = true;
					}
					int direction = self.direction;
					if (self.velocity.X < 0f)
					{
						direction = -1;
					}
					if (self.velocity.X > 0f)
					{
						direction = 1;
					}
					self.eocHit = i;
					if (self.whoAmI == Main.myPlayer)
					{
						self.ApplyDamageToNPC(hitNPC, (int)dmg, kb, direction, crit, DamageClass.Melee);
					}
					self.eocDash = 10;
					self.dashDelay = 30;
					self.velocity.X = -direction * 9;
					self.velocity.Y = -4f;
					self.GiveImmuneTimeForCollisionAttack(8);
					int heldDir = 0;
					if (self.controlLeft)
					{
						heldDir--;
					}
					if (self.controlRight)
					{
						heldDir++;
					}
					switch (Math.Abs(direction + heldDir))
					{
					case 0:
						self.velocity.X *= 1.75f;
						break;
					case 1:
						self.velocity.X *= 1.5f;
						break;
					case 2:
						self.velocity.X *= 1.25f;
						break;
					}
				}
			}
		}
		orig.Invoke(self);
	}

	private static void StardustGuardianAttackBuffs(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		for (int i = 0; i < 2; i++)
		{
			if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
			{
				(Instruction val) => ILPatternMatchingExt.MatchLdcR4(val, 100f)
			}))
			{
				LogFailure("Stardust Guardian Buffs", "Could not move to after the Stardust Guardian's attack range.");
				return;
			}
		}
		ILLabel label = il.DefineLabel();
		cursor.Emit(OpCodes.Ldloc_0);
		cursor.Emit(OpCodes.Ldfld, typeof(Player).GetField("wingsLogic"));
		cursor.Emit(OpCodes.Ldc_I4, 32);
		cursor.Emit(OpCodes.Bne_Un, (object)label);
		cursor.Emit(OpCodes.Ldc_R4, 960f);
		cursor.Emit(OpCodes.Stloc, 4);
		cursor.Emit(OpCodes.Ldc_R4, 960f);
		cursor.Emit(OpCodes.Stloc, 5);
		cursor.MarkLabel(label);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdcR4(val, 6f)
		}))
		{
			LogFailure("Stardust Guardian Buffs", "Could not locate the Stardust Guardian's attack move speed.");
			return;
		}
		cursor.EmitPop();
		cursor.Emit(OpCodes.Ldc_R4, 12f);
	}

	private static bool SolarWingsDashChange(orig_ConsumeSolarFlare orig, Player self)
	{
		if (orig.Invoke(self))
		{
			if (self.wingsLogic == 29)
			{
				self.wingTime += 60f;
			}
			return true;
		}
		return false;
	}

	private static void RemoveBeetleAndSolarFlareMultiplicativeDR(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "setSolar")
		}))
		{
			LogFailure("Melee Multiplicative DR Removal", "Could not locate the Solar Flare set bonus field.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.And);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "beetleDefense")
		}))
		{
			LogFailure("Melee Multiplicative DR Removal", "Could not locate the Beetle Shell set bonus field.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.And);
	}

	private static void RemoveFrozenInflictionFromDeerclopsIceSpikes(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 961)
		}))
		{
			LogFailure("Remove Frozen Infliction From Deerclops Ice Spikes", "Could not locate the Deerclops Ice Spike projectile ID.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.And);
	}

	private static bool GFBNurseMeteorUndodgeable(orig_IsDamageDodgable orig, Projectile self)
	{
		if (self.type == ModContent.ProjectileType<LeviathanBomb>() && self.damage == 9999)
		{
			return false;
		}
		return orig.Invoke(self);
	}

	void ILoadable.Load(Mod mod)
	{
		ManipulatorManager.ApplyEdits += ApplyEdits;
	}

	void ILoadable.Unload()
	{
	}

	private static void ApplyEdits(ManipulatorContext ctx)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected O, but got Unknown
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Expected O, but got Unknown
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Expected O, but got Unknown
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Expected O, but got Unknown
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Expected O, but got Unknown
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Expected O, but got Unknown
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Expected O, but got Unknown
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Expected O, but got Unknown
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Expected O, but got Unknown
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Expected O, but got Unknown
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Expected O, but got Unknown
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Expected O, but got Unknown
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Expected O, but got Unknown
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Expected O, but got Unknown
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Expected O, but got Unknown
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Expected O, but got Unknown
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Expected O, but got Unknown
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected O, but got Unknown
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Expected O, but got Unknown
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Expected O, but got Unknown
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Expected O, but got Unknown
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Expected O, but got Unknown
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Expected O, but got Unknown
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Expected O, but got Unknown
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Expected O, but got Unknown
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Expected O, but got Unknown
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Expected O, but got Unknown
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Expected O, but got Unknown
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Expected O, but got Unknown
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Expected O, but got Unknown
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Expected O, but got Unknown
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Expected O, but got Unknown
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Expected O, but got Unknown
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Expected O, but got Unknown
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Expected O, but got Unknown
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Expected O, but got Unknown
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Expected O, but got Unknown
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Expected O, but got Unknown
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Expected O, but got Unknown
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Expected O, but got Unknown
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Expected O, but got Unknown
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_092d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Expected O, but got Unknown
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Expected O, but got Unknown
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_096d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0973: Expected O, but got Unknown
		//IL_0988: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Expected O, but got Unknown
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Expected O, but got Unknown
		//IL_09c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Expected O, but got Unknown
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Expected O, but got Unknown
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Expected O, but got Unknown
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a33: Expected O, but got Unknown
		//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Expected O, but got Unknown
		//IL_0a68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Expected O, but got Unknown
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Expected O, but got Unknown
		//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Expected O, but got Unknown
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Expected O, but got Unknown
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Expected O, but got Unknown
		//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Expected O, but got Unknown
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Expected O, but got Unknown
		//IL_0b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Expected O, but got Unknown
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7a: Expected O, but got Unknown
		//IL_0b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9a: Expected O, but got Unknown
		//IL_0baf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bba: Expected O, but got Unknown
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Expected O, but got Unknown
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfa: Expected O, but got Unknown
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1a: Expected O, but got Unknown
		//IL_0c2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3a: Expected O, but got Unknown
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Expected O, but got Unknown
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Expected O, but got Unknown
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Expected O, but got Unknown
		//IL_0caf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Expected O, but got Unknown
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cda: Expected O, but got Unknown
		//IL_0cef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfa: Expected O, but got Unknown
		//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1a: Expected O, but got Unknown
		//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Expected O, but got Unknown
		//IL_0d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5a: Expected O, but got Unknown
		labDoorOpen = ModContent.TileType<LaboratoryDoorOpen>();
		labDoorClosed = ModContent.TileType<LaboratoryDoorClosed>();
		aLabDoorOpen = ModContent.TileType<AgedLaboratoryDoorOpen>();
		aLabDoorClosed = ModContent.TileType<AgedLaboratoryDoorClosed>();
		exoDoorOpen = ModContent.TileType<ExoDoorOpen>();
		exoDoorClosed = ModContent.TileType<ExoDoorClosed>();
		object obj = _003C_003EO._003C1_003E__CustomDoDrawChanges;
		if (obj == null)
		{
			Manipulator val = CustomDoDrawChanges;
			_003C_003EO._003C1_003E__CustomDoDrawChanges = val;
			obj = (object)val;
		}
		IL_Main.DoDraw += (Manipulator)obj;
		object obj2 = _003C_003EO._003C2_003E__UseCoolFireCursorEffect;
		if (obj2 == null)
		{
			hook_DrawCursor val2 = UseCoolFireCursorEffect;
			_003C_003EO._003C2_003E__UseCoolFireCursorEffect = val2;
			obj2 = (object)val2;
		}
		On_Main.DrawCursor += (hook_DrawCursor)obj2;
		object obj3 = _003C_003EO._003C3_003E__DrawFusableParticles;
		if (obj3 == null)
		{
			hook_SortDrawCacheWorms val3 = DrawFusableParticles;
			_003C_003EO._003C3_003E__DrawFusableParticles = val3;
			obj3 = (object)val3;
		}
		On_Main.SortDrawCacheWorms += (hook_SortDrawCacheWorms)obj3;
		object obj4 = _003C_003EO._003C4_003E__ClearTilePings;
		if (obj4 == null)
		{
			hook_Draw val4 = ClearTilePings;
			_003C_003EO._003C4_003E__ClearTilePings = val4;
			obj4 = (object)val4;
		}
		On_TileDrawing.Draw += (hook_Draw)obj4;
		object obj5 = _003C_003EO._003C5_003E__ColorBlightedGel;
		if (obj5 == null)
		{
			hook_ModifyItemDropFromNPC val5 = ColorBlightedGel;
			_003C_003EO._003C5_003E__ColorBlightedGel = val5;
			obj5 = (object)val5;
		}
		On_CommonCode.ModifyItemDropFromNPC += (hook_ModifyItemDropFromNPC)obj5;
		object obj6 = _003C_003EO._003C6_003E__DisableFlashesWithPhotosensitivityConfig;
		if (obj6 == null)
		{
			hook_RequestLight val6 = DisableFlashesWithPhotosensitivityConfig;
			_003C_003EO._003C6_003E__DisableFlashesWithPhotosensitivityConfig = val6;
			obj6 = (object)val6;
		}
		On_MoonlordDeathDrama.RequestLight += (hook_RequestLight)obj6;
		object obj7 = _003C_003EO._003C7_003E__DisableCullingForTreeAndCactus;
		if (obj7 == null)
		{
			Manipulator val7 = DisableCullingForTreeAndCactus;
			_003C_003EO._003C7_003E__DisableCullingForTreeAndCactus = val7;
			obj7 = (object)val7;
		}
		IL_TileDrawing.DrawSingleTile += (Manipulator)obj7;
		object obj8 = _003C_003EO._003C8_003E__DrawTreeGlowMask;
		if (obj8 == null)
		{
			Manipulator val8 = DrawTreeGlowMask;
			_003C_003EO._003C8_003E__DrawTreeGlowMask = val8;
			obj8 = (object)val8;
		}
		IL_TileDrawing.DrawTrees += (Manipulator)obj8;
		object obj9 = _003C_003EO._003C9_003E__DrawTreeTrunkAndCactusGlowMask;
		if (obj9 == null)
		{
			hook_DrawBasicTile val9 = DrawTreeTrunkAndCactusGlowMask;
			_003C_003EO._003C9_003E__DrawTreeTrunkAndCactusGlowMask = val9;
			obj9 = (object)val9;
		}
		On_TileDrawing.DrawBasicTile += (hook_DrawBasicTile)obj9;
		object obj10 = _003C_003EO._003C10_003E__PermitNighttimeTownNPCSpawning;
		if (obj10 == null)
		{
			Manipulator val10 = PermitNighttimeTownNPCSpawning;
			_003C_003EO._003C10_003E__PermitNighttimeTownNPCSpawning = val10;
			obj10 = (object)val10;
		}
		IL_Main.UpdateTime += (Manipulator)obj10;
		object obj11 = _003C_003EO._003C11_003E__AlterTownNPCSpawnRate;
		if (obj11 == null)
		{
			hook_UpdateTime_SpawnTownNPCs val11 = AlterTownNPCSpawnRate;
			_003C_003EO._003C11_003E__AlterTownNPCSpawnRate = val11;
			obj11 = (object)val11;
		}
		On_Main.UpdateTime_SpawnTownNPCs += (hook_UpdateTime_SpawnTownNPCs)obj11;
		object obj12 = _003C_003EO._003C12_003E__PreventVanillaBossDeathsInBossRush;
		if (obj12 == null)
		{
			Manipulator val12 = PreventVanillaBossDeathsInBossRush;
			_003C_003EO._003C12_003E__PreventVanillaBossDeathsInBossRush = val12;
			obj12 = (object)val12;
		}
		IL_NPC.DoDeathEvents += (Manipulator)obj12;
		object obj13 = _003C_003EO._003C13_003E__PreventDiabolistLootLogic;
		if (obj13 == null)
		{
			hook_NPCLoot val13 = PreventDiabolistLootLogic;
			_003C_003EO._003C13_003E__PreventDiabolistLootLogic = val13;
			obj13 = (object)val13;
		}
		On_NPC.NPCLoot += (hook_NPCLoot)obj13;
		object obj14 = _003C_003EO._003C14_003E__MakeTaxCollectorUseful;
		if (obj14 == null)
		{
			Manipulator val14 = MakeTaxCollectorUseful;
			_003C_003EO._003C14_003E__MakeTaxCollectorUseful = val14;
			obj14 = (object)val14;
		}
		IL_Player.CollectTaxes += (Manipulator)obj14;
		object obj15 = _003C_003EO._003C15_003E__AllowFusionFeederToDigThroughSand;
		if (obj15 == null)
		{
			hook_ApplyTileCollision val15 = AllowFusionFeederToDigThroughSand;
			_003C_003EO._003C15_003E__AllowFusionFeederToDigThroughSand = val15;
			obj15 = (object)val15;
		}
		On_NPC.ApplyTileCollision += (hook_ApplyTileCollision)obj15;
		object obj16 = _003C_003EO._003C16_003E__ScopesRequireVisibilityToZoom;
		if (obj16 == null)
		{
			Manipulator val16 = ScopesRequireVisibilityToZoom;
			_003C_003EO._003C16_003E__ScopesRequireVisibilityToZoom = val16;
			obj16 = (object)val16;
		}
		IL_Player.ApplyEquipFunctional += (Manipulator)obj16;
		object obj17 = _003C_003EO._003C17_003E__DodgeMechanicAdjustments;
		if (obj17 == null)
		{
			Manipulator val17 = DodgeMechanicAdjustments;
			_003C_003EO._003C17_003E__DodgeMechanicAdjustments = val17;
			obj17 = (object)val17;
		}
		IL_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float += (Manipulator)obj17;
		object obj18 = _003C_003EO._003C18_003E__AddHolyProtectionCooldown;
		if (obj18 == null)
		{
			hook_PutHallowedArmorSetBonusOnCooldown val18 = AddHolyProtectionCooldown;
			_003C_003EO._003C18_003E__AddHolyProtectionCooldown = val18;
			obj18 = (object)val18;
		}
		On_Player.PutHallowedArmorSetBonusOnCooldown += (hook_PutHallowedArmorSetBonusOnCooldown)obj18;
		object obj19 = _003C_003EO._003C19_003E__FixAllDashMechanics;
		if (obj19 == null)
		{
			Manipulator val19 = FixAllDashMechanics;
			_003C_003EO._003C19_003E__FixAllDashMechanics = val19;
			obj19 = (object)val19;
		}
		IL_Player.DashMovement += (Manipulator)obj19;
		object obj20 = _003C_003EO._003C20_003E__DashMovementEdits;
		if (obj20 == null)
		{
			hook_DashMovement val20 = DashMovementEdits;
			_003C_003EO._003C20_003E__DashMovementEdits = val20;
			obj20 = (object)val20;
		}
		On_Player.DashMovement += (hook_DashMovement)obj20;
		object obj21 = _003C_003EO._003C21_003E__ApplyDashKeybind;
		if (obj21 == null)
		{
			hook_DoCommonDashHandle val21 = ApplyDashKeybind;
			_003C_003EO._003C21_003E__ApplyDashKeybind = val21;
			obj21 = (object)val21;
		}
		On_Player.DoCommonDashHandle += (hook_DoCommonDashHandle)obj21;
		object obj22 = _003C_003EO._003C22_003E__DisableDoubleTapOnConfig;
		if (obj22 == null)
		{
			hook_KeyDoubleTap val22 = DisableDoubleTapOnConfig;
			_003C_003EO._003C22_003E__DisableDoubleTapOnConfig = val22;
			obj22 = (object)val22;
		}
		On_Player.KeyDoubleTap += (hook_KeyDoubleTap)obj22;
		object obj23 = _003C_003EO._003C23_003E__MakeShieldSlamIFramesConsistent;
		if (obj23 == null)
		{
			Manipulator val23 = MakeShieldSlamIFramesConsistent;
			_003C_003EO._003C23_003E__MakeShieldSlamIFramesConsistent = val23;
			obj23 = (object)val23;
		}
		IL_Player.GiveImmuneTimeForCollisionAttack += (Manipulator)obj23;
		object obj24 = _003C_003EO._003C24_003E__NerfShieldOfCthulhuBonkSafety;
		if (obj24 == null)
		{
			Manipulator val24 = NerfShieldOfCthulhuBonkSafety;
			_003C_003EO._003C24_003E__NerfShieldOfCthulhuBonkSafety = val24;
			obj24 = (object)val24;
		}
		IL_Player.Update_NPCCollision += (Manipulator)obj24;
		object obj25 = _003C_003EO._003C25_003E__OpenDoor_LabDoorOverride;
		if (obj25 == null)
		{
			hook_OpenDoor val25 = OpenDoor_LabDoorOverride;
			_003C_003EO._003C25_003E__OpenDoor_LabDoorOverride = val25;
			obj25 = (object)val25;
		}
		On_WorldGen.OpenDoor += (hook_OpenDoor)obj25;
		object obj26 = _003C_003EO._003C26_003E__CloseDoor_LabDoorOverride;
		if (obj26 == null)
		{
			hook_CloseDoor val26 = CloseDoor_LabDoorOverride;
			_003C_003EO._003C26_003E__CloseDoor_LabDoorOverride = val26;
			obj26 = (object)val26;
		}
		On_WorldGen.CloseDoor += (hook_CloseDoor)obj26;
		object obj27 = _003C_003EO._003C27_003E__IncorporateEnchantmentInAffix;
		if (obj27 == null)
		{
			hook_AffixName val27 = IncorporateEnchantmentInAffix;
			_003C_003EO._003C27_003E__IncorporateEnchantmentInAffix = val27;
			obj27 = (object)val27;
		}
		On_Item.AffixName += (hook_AffixName)obj27;
		object obj28 = _003C_003EO._003C28_003E__IncorporateExtraProjectileVariables;
		if (obj28 == null)
		{
			hook_NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float_float val28 = IncorporateExtraProjectileVariables;
			_003C_003EO._003C28_003E__IncorporateExtraProjectileVariables = val28;
			obj28 = (object)val28;
		}
		On_Projectile.NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float_float += (hook_NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float_float)obj28;
		object obj29 = _003C_003EO._003C29_003E__ApplyOldFashionedDamageToMiscHits;
		if (obj29 == null)
		{
			hook_ApplyDamageToNPC val29 = ApplyOldFashionedDamageToMiscHits;
			_003C_003EO._003C29_003E__ApplyOldFashionedDamageToMiscHits = val29;
			obj29 = (object)val29;
		}
		On_Player.ApplyDamageToNPC += (hook_ApplyDamageToNPC)obj29;
		object obj30 = _003C_003EO._003C30_003E__AddTwinklersToStatue;
		if (obj30 == null)
		{
			Manipulator val30 = AddTwinklersToStatue;
			_003C_003EO._003C30_003E__AddTwinklersToStatue = val30;
			obj30 = (object)val30;
		}
		IL_Wiring.HitWireSingle += (Manipulator)obj30;
		object obj31 = _003C_003EO._003C31_003E__GetDifficultyOverride;
		if (obj31 == null)
		{
			hook_GetDifficulty val31 = GetDifficultyOverride;
			_003C_003EO._003C31_003E__GetDifficultyOverride = val31;
			obj31 = (object)val31;
		}
		On_AWorldListItem.GetDifficulty += (hook_GetDifficulty)obj31;
		object obj32 = _003C_003EO._003C32_003E__ShimmerEffectEdits;
		if (obj32 == null)
		{
			hook_GetShimmered val32 = ShimmerEffectEdits;
			_003C_003EO._003C32_003E__ShimmerEffectEdits = val32;
			obj32 = (object)val32;
		}
		On_Item.GetShimmered += (hook_GetShimmered)obj32;
		object obj33 = _003C_003EO._003C33_003E__TPOverride;
		if (obj33 == null)
		{
			hook_Teleport val33 = TPOverride;
			_003C_003EO._003C33_003E__TPOverride = val33;
			obj33 = (object)val33;
		}
		On_Player.Teleport += (hook_Teleport)obj33;
		object obj34 = _003C_003EO._003C34_003E__GlowMaskTileRender;
		if (obj34 == null)
		{
			hook_DrawSingleTile val34 = GlowMaskTileRender;
			_003C_003EO._003C34_003E__GlowMaskTileRender = val34;
			obj34 = (object)val34;
		}
		On_TileDrawing.DrawSingleTile += (hook_DrawSingleTile)obj34;
		object obj35 = _003C_003EO._003C35_003E__AllowCannonJellyfishUse;
		if (obj35 == null)
		{
			hook_PlaceThing_CannonBall val35 = AllowCannonJellyfishUse;
			_003C_003EO._003C35_003E__AllowCannonJellyfishUse = val35;
			obj35 = (object)val35;
		}
		On_Player.PlaceThing_CannonBall += (hook_PlaceThing_CannonBall)obj35;
		object obj36 = _003C_003EO._003C36_003E__MasterModeCelestialOnionCheck;
		if (obj36 == null)
		{
			hook_IsItemSlotUnlockedAndUsable val36 = MasterModeCelestialOnionCheck;
			_003C_003EO._003C36_003E__MasterModeCelestialOnionCheck = val36;
			obj36 = (object)val36;
		}
		On_Player.IsItemSlotUnlockedAndUsable += (hook_IsItemSlotUnlockedAndUsable)obj36;
		object obj37 = _003C_003EO._003C37_003E__AllowHooksToGrabArenabox;
		if (obj37 == null)
		{
			hook_AI_007_GrapplingHooks val37 = AllowHooksToGrabArenabox;
			_003C_003EO._003C37_003E__AllowHooksToGrabArenabox = val37;
			obj37 = (object)val37;
		}
		On_Projectile.AI_007_GrapplingHooks += (hook_AI_007_GrapplingHooks)obj37;
		object obj38 = _003C_003EO._003C38_003E__ArenaCollision_Vector2_int_int;
		if (obj38 == null)
		{
			hook_SolidCollision_Vector2_int_int val38 = ArenaCollision_Vector2_int_int;
			_003C_003EO._003C38_003E__ArenaCollision_Vector2_int_int = val38;
			obj38 = (object)val38;
		}
		On_Collision.SolidCollision_Vector2_int_int += (hook_SolidCollision_Vector2_int_int)obj38;
		object obj39 = _003C_003EO._003C39_003E__ArenaCollision_Vector2_int_int_bool;
		if (obj39 == null)
		{
			hook_SolidCollision_Vector2_int_int_bool val39 = ArenaCollision_Vector2_int_int_bool;
			_003C_003EO._003C39_003E__ArenaCollision_Vector2_int_int_bool = val39;
			obj39 = (object)val39;
		}
		On_Collision.SolidCollision_Vector2_int_int_bool += (hook_SolidCollision_Vector2_int_int_bool)obj39;
		object obj40 = _003C_003EO._003C40_003E__ArenaCollision_TileCollision;
		if (obj40 == null)
		{
			hook_TileCollision val40 = ArenaCollision_TileCollision;
			_003C_003EO._003C40_003E__ArenaCollision_TileCollision = val40;
			obj40 = (object)val40;
		}
		On_Collision.TileCollision += (hook_TileCollision)obj40;
		object obj41 = _003C_003EO._003C41_003E__ChaliceBufferHeal;
		if (obj41 == null)
		{
			Manipulator val41 = ChaliceBufferHeal;
			_003C_003EO._003C41_003E__ChaliceBufferHeal = val41;
			obj41 = (object)val41;
		}
		IL_Player.ApplyLifeAndOrMana += (Manipulator)obj41;
		object obj42 = _003C_003EO._003C42_003E__AllowNegativeCheckMana;
		if (obj42 == null)
		{
			hook_CheckMana_int_bool_bool val42 = AllowNegativeCheckMana;
			_003C_003EO._003C42_003E__AllowNegativeCheckMana = val42;
			obj42 = (object)val42;
		}
		On_Player.CheckMana_int_bool_bool += (hook_CheckMana_int_bool_bool)obj42;
		object obj43 = _003C_003EO._003C43_003E__AllowNegativeCheckMana;
		if (obj43 == null)
		{
			hook_CheckMana_Item_int_bool_bool val43 = AllowNegativeCheckMana;
			_003C_003EO._003C43_003E__AllowNegativeCheckMana = val43;
			obj43 = (object)val43;
		}
		On_Player.CheckMana_Item_int_bool_bool += (hook_CheckMana_Item_int_bool_bool)obj43;
		object obj44 = _003C_003EO._003C44_003E__CustomGrappleMovementCheck;
		if (obj44 == null)
		{
			hook_GrappleMovement val44 = CustomGrappleMovementCheck;
			_003C_003EO._003C44_003E__CustomGrappleMovementCheck = val44;
			obj44 = (object)val44;
		}
		On_Player.GrappleMovement += (hook_GrappleMovement)obj44;
		object obj45 = _003C_003EO._003C45_003E__CustomGrapplePreDefaultMovement;
		if (obj45 == null)
		{
			hook_UpdatePettingAnimal val45 = CustomGrapplePreDefaultMovement;
			_003C_003EO._003C45_003E__CustomGrapplePreDefaultMovement = val45;
			obj45 = (object)val45;
		}
		On_Player.UpdatePettingAnimal += (hook_UpdatePettingAnimal)obj45;
		object obj46 = _003C_003EO._003C46_003E__CustomGrapplePostFrame;
		if (obj46 == null)
		{
			hook_PlayerFrame val46 = CustomGrapplePostFrame;
			_003C_003EO._003C46_003E__CustomGrapplePostFrame = val46;
			obj46 = (object)val46;
		}
		On_Player.PlayerFrame += (hook_PlayerFrame)obj46;
		object obj47 = _003C_003EO._003C47_003E__CustomGrapplePreStepUp;
		if (obj47 == null)
		{
			hook_SlopeDownMovement val47 = CustomGrapplePreStepUp;
			_003C_003EO._003C47_003E__CustomGrapplePreStepUp = val47;
			obj47 = (object)val47;
		}
		On_Player.SlopeDownMovement += (hook_SlopeDownMovement)obj47;
		object obj48 = _003C_003EO._003C48_003E__AdjustDamageVariance;
		if (obj48 == null)
		{
			hook_DamageVar_float_int_float val48 = AdjustDamageVariance;
			_003C_003EO._003C48_003E__AdjustDamageVariance = val48;
			obj48 = (object)val48;
		}
		On_Main.DamageVar_float_int_float += (hook_DamageVar_float_int_float)obj48;
		object obj49 = _003C_003EO._003C49_003E__RemoveExpertHardmodeScaling;
		if (obj49 == null)
		{
			Manipulator val49 = RemoveExpertHardmodeScaling;
			_003C_003EO._003C49_003E__RemoveExpertHardmodeScaling = val49;
			obj49 = (object)val49;
		}
		IL_NPC.ScaleStats_ApplyExpertTweaks += (Manipulator)obj49;
		object obj50 = _003C_003EO._003C50_003E__VanillaBossResistChanges;
		if (obj50 == null)
		{
			Manipulator val50 = VanillaBossResistChanges;
			_003C_003EO._003C50_003E__VanillaBossResistChanges = val50;
			obj50 = (object)val50;
		}
		IL_Projectile.Damage += (Manipulator)obj50;
		object obj51 = _003C_003EO._003C51_003E__LimitTerrarianProjectiles;
		if (obj51 == null)
		{
			Manipulator val51 = LimitTerrarianProjectiles;
			_003C_003EO._003C51_003E__LimitTerrarianProjectiles = val51;
			obj51 = (object)val51;
		}
		IL_Projectile.AI_099_2 += (Manipulator)obj51;
		object obj52 = _003C_003EO._003C52_003E__StardustGuardianAttackBuffs;
		if (obj52 == null)
		{
			Manipulator val52 = StardustGuardianAttackBuffs;
			_003C_003EO._003C52_003E__StardustGuardianAttackBuffs = val52;
			obj52 = (object)val52;
		}
		IL_Projectile.AI_120_StardustGuardian += (Manipulator)obj52;
		object obj53 = _003C_003EO._003C53_003E__SolarWingsDashChange;
		if (obj53 == null)
		{
			hook_ConsumeSolarFlare val53 = SolarWingsDashChange;
			_003C_003EO._003C53_003E__SolarWingsDashChange = val53;
			obj53 = (object)val53;
		}
		On_Player.ConsumeSolarFlare += (hook_ConsumeSolarFlare)obj53;
		object obj54 = _003C_003EO._003C54_003E__GFBNurseMeteorUndodgeable;
		if (obj54 == null)
		{
			hook_IsDamageDodgable val54 = GFBNurseMeteorUndodgeable;
			_003C_003EO._003C54_003E__GFBNurseMeteorUndodgeable = val54;
			obj54 = (object)val54;
		}
		On_Projectile.IsDamageDodgable += (hook_IsDamageDodgable)obj54;
		object obj55 = _003C_003EO._003C55_003E__UpdateBuffsBalancingChanges;
		if (obj55 == null)
		{
			Manipulator val55 = UpdateBuffsBalancingChanges;
			_003C_003EO._003C55_003E__UpdateBuffsBalancingChanges = val55;
			obj55 = (object)val55;
		}
		IL_Player.UpdateBuffs += (Manipulator)obj55;
		object obj56 = _003C_003EO._003C56_003E__RemoveBeetleAndSolarFlareMultiplicativeDR;
		if (obj56 == null)
		{
			Manipulator val56 = RemoveBeetleAndSolarFlareMultiplicativeDR;
			_003C_003EO._003C56_003E__RemoveBeetleAndSolarFlareMultiplicativeDR = val56;
			obj56 = (object)val56;
		}
		IL_Player.ApplyVanillaHurtEffectModifiers += (Manipulator)obj56;
		object obj57 = _003C_003EO._003C57_003E__FixJumpHeightBoosts;
		if (obj57 == null)
		{
			Manipulator val57 = FixJumpHeightBoosts;
			_003C_003EO._003C57_003E__FixJumpHeightBoosts = val57;
			obj57 = (object)val57;
		}
		IL_Player.UpdateJumpHeight += (Manipulator)obj57;
		ManipulatorBatch playerUpdate = ctx.PlayerUpdate;
		object obj58 = _003C_003EO._003C58_003E__BaseJumpSpeedAdjustment;
		if (obj58 == null)
		{
			Manipulator val58 = BaseJumpSpeedAdjustment;
			_003C_003EO._003C58_003E__BaseJumpSpeedAdjustment = val58;
			obj58 = (object)val58;
		}
		playerUpdate.Add((Manipulator)obj58);
		ManipulatorBatch playerUpdate2 = ctx.PlayerUpdate;
		object obj59 = _003C_003EO._003C59_003E__RunSpeedAdjustments;
		if (obj59 == null)
		{
			Manipulator val59 = RunSpeedAdjustments;
			_003C_003EO._003C59_003E__RunSpeedAdjustments = val59;
			obj59 = (object)val59;
		}
		playerUpdate2.Add((Manipulator)obj59);
		ManipulatorBatch playerUpdate3 = ctx.PlayerUpdate;
		object obj60 = _003C_003EO._003C60_003E__NerfOverpoweredRunAccelerationSources;
		if (obj60 == null)
		{
			Manipulator val60 = NerfOverpoweredRunAccelerationSources;
			_003C_003EO._003C60_003E__NerfOverpoweredRunAccelerationSources = val60;
			obj60 = (object)val60;
		}
		playerUpdate3.Add((Manipulator)obj60);
		object obj61 = _003C_003EO._003C61_003E__RemoveSoaringInsigniaInfiniteWingTime;
		if (obj61 == null)
		{
			Manipulator val61 = RemoveSoaringInsigniaInfiniteWingTime;
			_003C_003EO._003C61_003E__RemoveSoaringInsigniaInfiniteWingTime = val61;
			obj61 = (object)val61;
		}
		IL_Player.WingMovement += (Manipulator)obj61;
		object obj62 = _003C_003EO._003C62_003E__UpdateLifeRegenBalancingChanges;
		if (obj62 == null)
		{
			Manipulator val62 = UpdateLifeRegenBalancingChanges;
			_003C_003EO._003C62_003E__UpdateLifeRegenBalancingChanges = val62;
			obj62 = (object)val62;
		}
		IL_Player.UpdateLifeRegen += (Manipulator)obj62;
		object obj63 = _003C_003EO._003C63_003E__UpdateManaRegenBalancingChanges;
		if (obj63 == null)
		{
			Manipulator val63 = UpdateManaRegenBalancingChanges;
			_003C_003EO._003C63_003E__UpdateManaRegenBalancingChanges = val63;
			obj63 = (object)val63;
		}
		IL_Player.UpdateManaRegen += (Manipulator)obj63;
		ManipulatorBatch playerUpdate4 = ctx.PlayerUpdate;
		object obj64 = _003C_003EO._003C64_003E__ManaRegenDelayAdjustment;
		if (obj64 == null)
		{
			Manipulator val64 = ManaRegenDelayAdjustment;
			_003C_003EO._003C64_003E__ManaRegenDelayAdjustment = val64;
			obj64 = (object)val64;
		}
		playerUpdate4.Add((Manipulator)obj64);
		object obj65 = _003C_003EO._003C65_003E__RemoveFrozenInflictionFromDeerclopsIceSpikes;
		if (obj65 == null)
		{
			Manipulator val65 = RemoveFrozenInflictionFromDeerclopsIceSpikes;
			_003C_003EO._003C65_003E__RemoveFrozenInflictionFromDeerclopsIceSpikes = val65;
			obj65 = (object)val65;
		}
		IL_Projectile.StatusPlayer += (Manipulator)obj65;
		object obj66 = _003C_003EO._003C66_003E__BlockLivingTreesNearOcean;
		if (obj66 == null)
		{
			Manipulator val66 = BlockLivingTreesNearOcean;
			_003C_003EO._003C66_003E__BlockLivingTreesNearOcean = val66;
			obj66 = (object)val66;
		}
		IL_WorldGen.GrowLivingTree += (Manipulator)obj66;
		object obj67 = _003C_003EO._003C67_003E__PreventSmashAltarCode;
		if (obj67 == null)
		{
			hook_SmashAltar val67 = PreventSmashAltarCode;
			_003C_003EO._003C67_003E__PreventSmashAltarCode = val67;
			obj67 = (object)val67;
		}
		On_WorldGen.SmashAltar += (hook_SmashAltar)obj67;
		object obj68 = _003C_003EO._003C68_003E__AdjustChlorophyteSpawnRate;
		if (obj68 == null)
		{
			Manipulator val68 = AdjustChlorophyteSpawnRate;
			_003C_003EO._003C68_003E__AdjustChlorophyteSpawnRate = val68;
			obj68 = (object)val68;
		}
		IL_WorldGen.hardUpdateWorld += (Manipulator)obj68;
		object obj69 = _003C_003EO._003C69_003E__AdjustChlorophyteSpawnLimits;
		if (obj69 == null)
		{
			Manipulator val69 = AdjustChlorophyteSpawnLimits;
			_003C_003EO._003C69_003E__AdjustChlorophyteSpawnLimits = val69;
			obj69 = (object)val69;
		}
		IL_WorldGen.Chlorophyte += (Manipulator)obj69;
		object obj70 = _003C_003EO._003C70_003E__ChangeDefaultWorldSize;
		if (obj70 == null)
		{
			Manipulator val70 = ChangeDefaultWorldSize;
			_003C_003EO._003C70_003E__ChangeDefaultWorldSize = val70;
			obj70 = (object)val70;
		}
		IL_UIWorldCreation.SetDefaultOptions += (Manipulator)obj70;
		object obj71 = _003C_003EO._003C71_003E__SwapSmallDescriptionKey;
		if (obj71 == null)
		{
			Manipulator val71 = SwapSmallDescriptionKey;
			_003C_003EO._003C71_003E__SwapSmallDescriptionKey = val71;
			obj71 = (object)val71;
		}
		IL_UIWorldCreation.AddWorldSizeOptions += (Manipulator)obj71;
		object obj72 = _003C_003EO._003C72_003E__LimitDungeonEntranceXPosition;
		if (obj72 == null)
		{
			hook_MakeDungeon val72 = LimitDungeonEntranceXPosition;
			_003C_003EO._003C72_003E__LimitDungeonEntranceXPosition = val72;
			obj72 = (object)val72;
		}
		On_WorldGen.MakeDungeon += (hook_MakeDungeon)obj72;
		object obj73 = _003C_003EO._003C73_003E__LimitDungeonHallsXPosition;
		if (obj73 == null)
		{
			Manipulator val73 = LimitDungeonHallsXPosition;
			_003C_003EO._003C73_003E__LimitDungeonHallsXPosition = val73;
			obj73 = (object)val73;
		}
		IL_WorldGen.DungeonHalls += (Manipulator)obj73;
		object obj74 = _003C_003EO._003C74_003E__RemoveExpertBrainRandomDebuffs;
		if (obj74 == null)
		{
			Manipulator val74 = RemoveExpertBrainRandomDebuffs;
			_003C_003EO._003C74_003E__RemoveExpertBrainRandomDebuffs = val74;
			obj74 = (object)val74;
		}
		IL_Player.StatusFromNPC += (Manipulator)obj74;
		object obj75 = _003C_003EO._003C75_003E__PreventLavaSlimeLavaDrop;
		if (obj75 == null)
		{
			hook_HitEffect_HitInfo val75 = PreventLavaSlimeLavaDrop;
			_003C_003EO._003C75_003E__PreventLavaSlimeLavaDrop = val75;
			obj75 = (object)val75;
		}
		On_NPC.HitEffect_HitInfo += (hook_HitEffect_HitInfo)obj75;
		object obj76 = _003C_003EO._003C76_003E__LetDetonatingBubblesTakeDamage;
		if (obj76 == null)
		{
			Manipulator val76 = LetDetonatingBubblesTakeDamage;
			_003C_003EO._003C76_003E__LetDetonatingBubblesTakeDamage = val76;
			obj76 = (object)val76;
		}
		IL_NPC.StrikeNPC_HitInfo_bool_bool += (Manipulator)obj76;
		object obj77 = _003C_003EO._003C77_003E__PunchCameraUsesScreenshakeConfig;
		if (obj77 == null)
		{
			Manipulator val77 = PunchCameraUsesScreenshakeConfig;
			_003C_003EO._003C77_003E__PunchCameraUsesScreenshakeConfig = val77;
			obj77 = (object)val77;
		}
		IL_PunchCameraModifier.Update += (Manipulator)obj77;
		object obj78 = _003C_003EO._003C78_003E__MakeMagmaStoneFireGauntletDustToggleable;
		if (obj78 == null)
		{
			Manipulator val78 = MakeMagmaStoneFireGauntletDustToggleable;
			_003C_003EO._003C78_003E__MakeMagmaStoneFireGauntletDustToggleable = val78;
			obj78 = (object)val78;
		}
		IL_Player.ItemCheck_EmitUseVisuals += (Manipulator)obj78;
		object obj79 = _003C_003EO._003C79_003E__MakeMagmaStoneFireGauntletProjectileDustToggleable;
		if (obj79 == null)
		{
			Manipulator val79 = MakeMagmaStoneFireGauntletProjectileDustToggleable;
			_003C_003EO._003C79_003E__MakeMagmaStoneFireGauntletProjectileDustToggleable = val79;
			obj79 = (object)val79;
		}
		IL_Projectile.EmitEnchantmentVisualsAt += (Manipulator)obj79;
		object obj80 = _003C_003EO._003C80_003E__DecreaseSandstormWindSpeedRequirement;
		if (obj80 == null)
		{
			Manipulator val80 = DecreaseSandstormWindSpeedRequirement;
			_003C_003EO._003C80_003E__DecreaseSandstormWindSpeedRequirement = val80;
			obj80 = (object)val80;
		}
		IL_Sandstorm.HasSufficientWind += (Manipulator)obj80;
		object obj81 = _003C_003EO._003C81_003E__RelaxPrefixRequirements;
		if (obj81 == null)
		{
			Manipulator val81 = RelaxPrefixRequirements;
			_003C_003EO._003C81_003E__RelaxPrefixRequirements = val81;
			obj81 = (object)val81;
		}
		IL_Item.TryGetPrefixStatMultipliersForItem += (Manipulator)obj81;
		object obj82 = _003C_003EO._003C82_003E__PreventBossSlimeRainSpawns;
		if (obj82 == null)
		{
			hook_SlimeRainSpawns val82 = PreventBossSlimeRainSpawns;
			_003C_003EO._003C82_003E__PreventBossSlimeRainSpawns = val82;
			obj82 = (object)val82;
		}
		On_NPC.SlimeRainSpawns += (hook_SlimeRainSpawns)obj82;
		object obj83 = _003C_003EO._003C83_003E__AdjustShimmerRequirements;
		if (obj83 == null)
		{
			hook_IsItemTransformLocked val83 = AdjustShimmerRequirements;
			_003C_003EO._003C83_003E__AdjustShimmerRequirements = val83;
			obj83 = (object)val83;
		}
		On_ShimmerTransforms.IsItemTransformLocked += (hook_IsItemTransformLocked)obj83;
		object obj84 = _003C_003EO._003C84_003E__FlailsNoLongerAffectedByPlayerVelocity;
		if (obj84 == null)
		{
			hook_AI_015_Flails val84 = FlailsNoLongerAffectedByPlayerVelocity;
			_003C_003EO._003C84_003E__FlailsNoLongerAffectedByPlayerVelocity = val84;
			obj84 = (object)val84;
		}
		On_Projectile.AI_015_Flails += (hook_AI_015_Flails)obj84;
		object obj85 = _003C_003EO._003C85_003E__MakeMeteoriteExplodable;
		if (obj85 == null)
		{
			Manipulator val85 = MakeMeteoriteExplodable;
			_003C_003EO._003C85_003E__MakeMeteoriteExplodable = val85;
			obj85 = (object)val85;
		}
		IL_Projectile.CanExplodeTile += (Manipulator)obj85;
		object obj86 = _003C_003EO._003C86_003E__BloodMoonsRequire200MaxLife;
		if (obj86 == null)
		{
			Manipulator val86 = BloodMoonsRequire200MaxLife;
			_003C_003EO._003C86_003E__BloodMoonsRequire200MaxLife = val86;
			obj86 = (object)val86;
		}
		IL_Main.UpdateTime_StartNight += (Manipulator)obj86;
		object obj87 = _003C_003EO._003C87_003E__PreventFossilShattering;
		if (obj87 == null)
		{
			Manipulator val87 = PreventFossilShattering;
			_003C_003EO._003C87_003E__PreventFossilShattering = val87;
			obj87 = (object)val87;
		}
		IL_WorldGen.AttemptFossilShattering += (Manipulator)obj87;
		object obj88 = _003C_003EO._003C88_003E__RemoveHellforgePickaxeRequirement;
		if (obj88 == null)
		{
			hook_GetPickaxeDamage val88 = RemoveHellforgePickaxeRequirement;
			_003C_003EO._003C88_003E__RemoveHellforgePickaxeRequirement = val88;
			obj88 = (object)val88;
		}
		On_Player.GetPickaxeDamage += (hook_GetPickaxeDamage)obj88;
		ManipulatorBatch playerUpdate5 = ctx.PlayerUpdate;
		object obj89 = _003C_003EO._003C89_003E__PreventUFODismountInWater;
		if (obj89 == null)
		{
			Manipulator val89 = PreventUFODismountInWater;
			_003C_003EO._003C89_003E__PreventUFODismountInWater = val89;
			obj89 = (object)val89;
		}
		playerUpdate5.Add((Manipulator)obj89);
		object obj90 = _003C_003EO._003C90_003E__AddMoreGuaranteedAnglerRewards;
		if (obj90 == null)
		{
			hook_GetAnglerReward_MainReward val90 = AddMoreGuaranteedAnglerRewards;
			_003C_003EO._003C90_003E__AddMoreGuaranteedAnglerRewards = val90;
			obj90 = (object)val90;
		}
		On_Player.GetAnglerReward_MainReward += (hook_GetAnglerReward_MainReward)obj90;
		object obj91 = _003C_003EO._003C91_003E__ImproveAnglerBaitReward;
		if (obj91 == null)
		{
			hook_GetAnglerReward_Bait val91 = ImproveAnglerBaitReward;
			_003C_003EO._003C91_003E__ImproveAnglerBaitReward = val91;
			obj91 = (object)val91;
		}
		On_Player.GetAnglerReward_Bait += (hook_GetAnglerReward_Bait)obj91;
		object obj92 = _003C_003EO._003C92_003E__ImproveAnglerMoneyReward;
		if (obj92 == null)
		{
			hook_GetAnglerReward_Money val92 = ImproveAnglerMoneyReward;
			_003C_003EO._003C92_003E__ImproveAnglerMoneyReward = val92;
			obj92 = (object)val92;
		}
		On_Player.GetAnglerReward_Money += (hook_GetAnglerReward_Money)obj92;
		object obj93 = _003C_003EO._003C93_003E__RemovePowerCellPlanteraLock;
		if (obj93 == null)
		{
			Manipulator val93 = RemovePowerCellPlanteraLock;
			_003C_003EO._003C93_003E__RemovePowerCellPlanteraLock = val93;
			obj93 = (object)val93;
		}
		IL_Player.TileInteractionsUse += (Manipulator)obj93;
		object obj94 = _003C_003EO._003C94_003E__RemoveUseLocks;
		if (obj94 == null)
		{
			hook_ItemCheck_CheckCanUse val94 = RemoveUseLocks;
			_003C_003EO._003C94_003E__RemoveUseLocks = val94;
			obj94 = (object)val94;
		}
		On_Player.ItemCheck_CheckCanUse += (hook_ItemCheck_CheckCanUse)obj94;
		object obj95 = _003C_003EO._003C95_003E__ApplyCelestialSigilChanges;
		if (obj95 == null)
		{
			hook_ItemCheck_UseEventItems val95 = ApplyCelestialSigilChanges;
			_003C_003EO._003C95_003E__ApplyCelestialSigilChanges = val95;
			obj95 = (object)val95;
		}
		On_Player.ItemCheck_UseEventItems += (hook_ItemCheck_UseEventItems)obj95;
		object obj96 = _003C_003EO._003C96_003E__RemoveDamageConditionFromRadar;
		if (obj96 == null)
		{
			Manipulator val96 = RemoveDamageConditionFromRadar;
			_003C_003EO._003C96_003E__RemoveDamageConditionFromRadar = val96;
			obj96 = (object)val96;
		}
		IL_Main.DrawInfoAccs += (Manipulator)obj96;
		object obj97 = _003C_003EO._003C97_003E__DelayGravity;
		if (obj97 == null)
		{
			hook_UpdateControlHolds val97 = DelayGravity;
			_003C_003EO._003C97_003E__DelayGravity = val97;
			obj97 = (object)val97;
		}
		On_Player.UpdateControlHolds += (hook_UpdateControlHolds)obj97;
		object obj98 = _003C_003EO._003C98_003E__GravityMouse;
		if (obj98 == null)
		{
			hook_SetZoom_MouseInWorld val98 = GravityMouse;
			_003C_003EO._003C98_003E__GravityMouse = val98;
			obj98 = (object)val98;
		}
		On_PlayerInput.SetZoom_MouseInWorld += (hook_SetZoom_MouseInWorld)obj98;
		object obj99 = _003C_003EO._003C99_003E__UI_Unflip_Start;
		if (obj99 == null)
		{
			hook_DrawPlayerChatBubbles val99 = UI_Unflip_Start;
			_003C_003EO._003C99_003E__UI_Unflip_Start = val99;
			obj99 = (object)val99;
		}
		On_Main.DrawPlayerChatBubbles += (hook_DrawPlayerChatBubbles)obj99;
		object obj100 = _003C_003EO._003C100_003E__UI_Unflip_End;
		if (obj100 == null)
		{
			hook_DrawInterface val100 = UI_Unflip_End;
			_003C_003EO._003C100_003E__UI_Unflip_End = val100;
			obj100 = (object)val100;
		}
		On_Main.DrawInterface += (hook_DrawInterface)obj100;
		object obj101 = _003C_003EO._003C101_003E__FixHeldProjectileBlendState;
		if (obj101 == null)
		{
			hook_DrawHeldProj val101 = FixHeldProjectileBlendState;
			_003C_003EO._003C101_003E__FixHeldProjectileBlendState = val101;
			obj101 = (object)val101;
		}
		On_PlayerDrawLayers.DrawHeldProj += (hook_DrawHeldProj)obj101;
		object obj102 = _003C_003EO._003C102_003E__FixTruffleWormFishing;
		if (obj102 == null)
		{
			Manipulator val102 = FixTruffleWormFishing;
			_003C_003EO._003C102_003E__FixTruffleWormFishing = val102;
			obj102 = (object)val102;
		}
		IL_Player.ItemCheck_CheckFishingBobbers += (Manipulator)obj102;
		object obj103 = _003C_003EO._003C103_003E__UseVisibleThroughWaterMapTile;
		if (obj103 == null)
		{
			Manipulator val103 = UseVisibleThroughWaterMapTile;
			_003C_003EO._003C103_003E__UseVisibleThroughWaterMapTile = val103;
			obj103 = (object)val103;
		}
		IL_MapHelper.CreateMapTile += (Manipulator)obj103;
		object obj104 = _003C_003EO._003C104_003E__EnsureCheckDeadOnSegments;
		if (obj104 == null)
		{
			Manipulator val104 = EnsureCheckDeadOnSegments;
			_003C_003EO._003C104_003E__EnsureCheckDeadOnSegments = val104;
			obj104 = (object)val104;
		}
		IL_NPC.StrikeNPC_HitInfo_bool_bool += (Manipulator)obj104;
	}

	private static int FindTopOfDoor(int i, int j, Tile rootTile)
	{
		Tile t = Main.tile[i, j];
		int topY = j;
		while (t != null && t.HasTile && t.TileType == rootTile.TileType)
		{
			if (topY == 0)
			{
				return topY;
			}
			topY--;
			t = Main.tile[i, topY];
		}
		return ++topY;
	}

	private static bool OpenLabDoor(Tile tile, int i, int j, int openID)
	{
		int topY = FindTopOfDoor(i, j, tile);
		return DirectlyTransformLabDoor(i, topY, openID);
	}

	private static bool CloseLabDoor(Tile tile, int i, int j, int closedID)
	{
		int topY = FindTopOfDoor(i, j, tile);
		return DirectlyTransformLabDoor(i, topY, closedID);
	}

	private static bool DirectlyTransformLabDoor(int doorX, int doorY, int newDoorID, int wireHitY = -1)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		for (int y = doorY; y < doorY + 4; y++)
		{
			Main.tile[doorX, y].TileType = (ushort)newDoorID;
			if (Main.netMode != 1 && Wiring.running && y != wireHitY)
			{
				Wiring.SkipWire(doorX, y);
			}
		}
		for (int i = doorY; i < doorY + 4; i++)
		{
			WorldGen.TileFrame(doorX, i);
		}
		SoundEngine.PlaySound(in SoundID.DoorClosed, (Vector2?)new Vector2((float)(doorX * 16), (float)(doorY * 16)), (SoundUpdateCallback?)null);
		return true;
	}

	public static void DumpToLog(ILContext il)
	{
		CalamityMod.Log.Debug((object)((object)il).ToString());
	}

	public static void LogFailure(string name, string reason)
	{
		CalamityMod.Log.ILFailure(name, reason);
	}

	private static void MakeShieldSlamIFramesConsistent(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStfld<Player>(i, "immuneTime")
		}))
		{
			LogFailure("Shield Slam Consistent Immunity Frames", "Could not locate the assignment of the player's immune time.");
			return;
		}
		cursor.Remove();
		cursor.EmitDelegate<Action<Player, int>>((Action<Player, int>)delegate(Player p, int frames)
		{
			p.GiveUniversalIFrames(frames);
		});
	}

	private static void FixAllDashMechanics(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "dash")
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate the check for vanilla dash ID.");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.EmitDelegate<Func<Player, int>>(CalamityDashEquipped);
		cursor.Emit(OpCodes.Or);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 150f)
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate Solar Flare Armor shield slam base damage.");
			return;
		}
		cursor.Next.Operand = 400f;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 4)
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate Solar Flare shield slam iframes.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 12);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStloc(i, 22)
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate default dash cooldown initialization.");
			return;
		}
		cursor.Previous.Operand = 30;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStloc(i, 22)
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate dash cooldown assignment for Shield of Cthulhu.");
			return;
		}
		cursor.Previous.Operand = 30;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStloc(i, 22)
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate dash cooldown assignment for Solar Flare Armor.");
			return;
		}
		cursor.Previous.Operand = 30;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStloc(i, 22)
		}))
		{
			LogFailure("Vanilla Dash Fixes", "Could not locate dash cooldown assignment for UNKNOWN DASH ID 4.");
		}
		else
		{
			cursor.Previous.Operand = 30;
		}
	}

	private static void NerfShieldOfCthulhuBonkSafety(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "eocDash")
		}))
		{
			LogFailure("Shield of Cthulhu Bonk Nerf", "Could not locate Shield of Cthulhu dash remaining frame counter.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 0)
		}))
		{
			LogFailure("Shield of Cthulhu Bonk Nerf", "Could not locate the zero comparison.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 4);
	}

	private static void ApplyDashKeybind(orig_DoCommonDashHandle orig, Player self, out int dir, out bool dashing, Player.DashStartAction dashStartAction)
	{
		if (self.whoAmI != Main.myPlayer)
		{
			orig.Invoke(self, ref dir, ref dashing, dashStartAction);
		}
		else if (CalamityKeybinds.DashHotkey.JustPressed)
		{
			if (self.controlRight && !self.controlLeft)
			{
				self.direction = 1;
			}
			else if (self.controlLeft && !self.controlRight)
			{
				self.direction = -1;
			}
			else if (MathF.Abs(self.velocity.X) > 0.01f)
			{
				self.direction = ((self.velocity.X > 0f) ? 1 : (-1));
			}
			dir = self.direction;
			dashing = true;
			dashing = true;
			self.dashTime = 0;
			self.timeSinceLastDashStarted = 0;
			dashStartAction?.Invoke(dir);
		}
		else if (CalamityKeybinds.DashHotkey.GetAssignedKeysOrEmpty().Count == 0)
		{
			orig.Invoke(self, ref dir, ref dashing, dashStartAction);
		}
		else
		{
			dir = 1;
			dashing = false;
			self.dashTime = 0;
		}
	}

	private static void DisableDoubleTapOnConfig(orig_KeyDoubleTap orig, Player self, int keyDir)
	{
		if (self.whoAmI != Main.myPlayer)
		{
			orig.Invoke(self, keyDir);
		}
		else if ((CalamityKeybinds.ArmorSetBonusHotKey.GetAssignedKeysOrEmpty().Count == 0 || CalamityClientConfig.Instance.SetBonusDoubleTap != SetBonusDoubleTapOptions.Auto) && CalamityClientConfig.Instance.SetBonusDoubleTap != SetBonusDoubleTapOptions.Off)
		{
			orig.Invoke(self, keyDir);
		}
	}

	private static void PreventVanillaBossDeathsInBossRush(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchCall<NPC>(val, "SpawnOnPlayer")
		}))
		{
			LogFailure("Prevent Vanilla Defeated Flags in Boss Rush", "Could not move to before the switch case.");
			return;
		}
		ILLabel label = il.DefineLabel();
		cursor.Emit(OpCodes.Ldsfld, typeof(BossRushEvent).GetField("BossRushActive"));
		cursor.Emit(OpCodes.Brtrue, (object)label);
		for (int i = 0; i < 2; i++)
		{
			if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
			{
				(Instruction val) => ILPatternMatchingExt.MatchCall<NPC>(val, "SpawnBoss")
			}))
			{
				LogFailure("Prevent Vanilla Defeated Flags in Boss Rush", "Could not move to after the switch case.");
				return;
			}
		}
		if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdarg0(val)
		}))
		{
			LogFailure("Prevent Vanilla Defeated Flags in Boss Rush", "Could not move to after the switch case.");
		}
		else
		{
			cursor.MarkLabel(label);
		}
	}

	private static void AllowFusionFeederToDigThroughSand(orig_ApplyTileCollision orig, NPC self, bool fall, Vector2 cPosition, int cWidth, int cHeight)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (self.active && self.type == ModContent.NPCType<FusionFeeder>())
		{
			self.velocity = Collision.AdvancedTileCollision(TileID.Sets.ForAdvancedCollision.ForSandshark, cPosition, self.velocity, cWidth, cHeight, fall, fall);
		}
		else
		{
			orig.Invoke(self, fall, cPosition, cWidth, cHeight);
		}
	}

	private static void PreventDiabolistLootLogic(orig_NPCLoot orig, NPC self)
	{
		if (self.type != 286 || !Main.getGoodWorld || NPC.downedPlantBoss)
		{
			orig.Invoke(self);
		}
	}

	private static void PermitNighttimeTownNPCSpawning(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		cursor.EmitDelegate<Action>((Action)delegate
		{
			if (Main.dayTime || CalamityServerConfig.Instance.TownNPCsSpawnAtNight)
			{
				Main.UpdateTime_SpawnTownNPCs();
			}
		});
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCallOrCallvirt<Main>(i, "UpdateTime_SpawnTownNPCs")
		}))
		{
			CalamityMod.Log.Warn((object)"Town NPC spawn editing code failed.");
		}
		else
		{
			cursor.Emit(OpCodes.Ret);
		}
	}

	private static void AlterTownNPCSpawnRate(orig_UpdateTime_SpawnTownNPCs orig)
	{
		double desiredWorldTilesUpdateRate = Main.desiredWorldTilesUpdateRate;
		Main.desiredWorldTilesUpdateRate *= CalamityServerConfig.Instance.TownNPCSpawnRateMultiplier;
		orig.Invoke();
		Main.desiredWorldTilesUpdateRate = desiredWorldTilesUpdateRate;
	}

	private static void DodgeMechanicAdjustments(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall(i, typeof(Player.HurtModifiers), "ToHurtInfo")
		}))
		{
			LogFailure("Dodge Mechanic Adjustments", "Could not locate the call to HurtModifiers.ToHurtInfo.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) =>
			{
				ILLabel val = default(ILLabel);
				return ILPatternMatchingExt.MatchBrfalse(i, ref val);
			}
		}))
		{
			LogFailure("Dodge Mechanic Adjustments", "Could not locate the dodgeable boolean branch.");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.EmitDelegate<Func<Player, bool>>((Func<Player, bool>)((Player p) => !p.Calamity().disableAllDodges));
		cursor.Emit(OpCodes.And);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "blackBelt")
		}))
		{
			LogFailure("Dodge Mechanic Adjustments", "Could not locate the Black Belt equipped boolean.");
			return;
		}
		cursor.Emit(OpCodes.Pop);
		cursor.Emit(OpCodes.Ldc_I4_0);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "brainOfConfusionItem")
		}))
		{
			LogFailure("Dodge Mechanic Adjustments", "Could not locate the Brain of Confusion tracked equipped item.");
			return;
		}
		cursor.Emit(OpCodes.Pop);
		cursor.Emit(OpCodes.Ldc_I4_0);
	}

	private static void AddHolyProtectionCooldown(orig_PutHallowedArmorSetBonusOnCooldown orig, Player self)
	{
		orig.Invoke(self);
		self.AddCooldown(HolyProtection.ID, CalamityUtils.SecondsToFrames(30));
	}

	private static bool OpenDoor_LabDoorOverride(orig_OpenDoor orig, int i, int j, int direction)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileType == labDoorClosed)
		{
			return OpenLabDoor(tile, i, j, labDoorOpen);
		}
		if (tile.TileType == aLabDoorClosed)
		{
			return OpenLabDoor(tile, i, j, aLabDoorOpen);
		}
		if (tile.TileType == exoDoorClosed)
		{
			return OpenLabDoor(tile, i, j, exoDoorOpen);
		}
		return orig.Invoke(i, j, direction);
	}

	private static bool CloseDoor_LabDoorOverride(orig_CloseDoor orig, int i, int j, bool forced)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileType == labDoorOpen)
		{
			return CloseLabDoor(tile, i, j, labDoorClosed);
		}
		if (tile.TileType == aLabDoorOpen)
		{
			return CloseLabDoor(tile, i, j, aLabDoorClosed);
		}
		if (tile.TileType == exoDoorOpen)
		{
			return CloseLabDoor(tile, i, j, exoDoorClosed);
		}
		return orig.Invoke(i, j, forced);
	}

	private static string IncorporateEnchantmentInAffix(orig_AffixName orig, Item self)
	{
		string result = orig.Invoke(self);
		try
		{
			if (!self.IsAir && self.Calamity().AppliedEnchantment.HasValue)
			{
				result = $"{self.Calamity().AppliedEnchantment.Value.Name} {result}";
			}
		}
		catch
		{
		}
		return result;
	}

	private static int IncorporateExtraProjectileVariables(orig_NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float_float orig, IEntitySource spawnSource, float x, float y, float xSpeed, float ySpeed, int type, int damage, float knockback, int owner, float ai0, float ai1, float ai2)
	{
		int proj = orig.Invoke(spawnSource, x, y, xSpeed, ySpeed, type, damage, knockback, owner, ai0, ai1, ai2);
		Projectile projectile = Main.projectile[proj];
		Player player = Main.player[projectile.owner];
		if (Main.gameMenu || !player.active)
		{
			return proj;
		}
		if (!projectile.TryGetGlobalProjectile<CalamityGlobalProjectile>(out var calProj))
		{
			return proj;
		}
		if (spawnSource is EntitySource_Parent parentSource)
		{
			if (parentSource.Entity is Item { damage: >0, useAnimation: >0 })
			{
				calProj.buffedByOldFashioned = false;
			}
			else if (parentSource.Entity is Player parentPlayer && parentPlayer.whoAmI == projectile.owner)
			{
				if (spawnSource is EntitySource_ItemUse itemSource)
				{
					Item usedItem = itemSource.Item;
					if (usedItem != null && usedItem.damage > 0 && usedItem.useAnimation > 0 && usedItem.type != ModContent.ItemType<WulfrumFusionCannon>())
					{
						calProj.buffedByOldFashioned = false;
						goto IL_0130;
					}
				}
				calProj.buffedByOldFashioned = true;
			}
			else if (parentSource.Entity is Projectile parentProj)
			{
				calProj.buffedByOldFashioned = parentProj.Calamity().buffedByOldFashioned;
			}
		}
		goto IL_0130;
		IL_0130:
		if (projectile.minion && spawnSource is EntitySource_ItemUse useSource)
		{
			Item weapon = useSource.Item;
			if (weapon != null && weapon.useAnimation > 0)
			{
				CalamityPlayer.EnchantHeldItemEffects(player, player.Calamity(), player.HeldItem);
				if (player.Calamity().explosiveMinionsEnchant)
				{
					calProj.ExplosiveEnchantCountdown = 2400;
				}
				goto IL_01c5;
			}
		}
		if (ProjectileID.Sets.MinionShot[projectile.type] && spawnSource is EntitySource_Parent { Entity: Projectile parent })
		{
			calProj.ExplosiveEnchantCountdown = parent.Calamity().ExplosiveEnchantCountdown;
		}
		goto IL_01c5;
		IL_01c5:
		return proj;
	}

	private static void ApplyOldFashionedDamageToMiscHits(orig_ApplyDamageToNPC orig, Player self, NPC npc, int damage, float knockback, int direction, bool crit = false, DamageClass? damageType = null, bool damageVariation = false)
	{
		if (self.Calamity().oldFashioned)
		{
			damage = (int)((float)damage * OldFashioned.DamageBoostMultiplier);
		}
		orig.Invoke(self, npc, damage, knockback, direction, crit, damageType, damageVariation);
	}

	private static void ChaliceBufferHeal(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction c) => ILPatternMatchingExt.MatchStfld<Player>(c, "statLife")
		}))
		{
			LogFailure("Chalice of the Blood God Bleedout Heal", "Could not locate the player's health being restored");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.Emit(OpCodes.Ldarg_1);
		cursor.EmitDelegate<Action<Player, Item>>((Action<Player, Item>)delegate(Player player, Item potion)
		{
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			if (player.active && !player.dead && potion.healLife > 0)
			{
				CalamityPlayer calamityPlayer = player.Calamity();
				if (calamityPlayer != null && calamityPlayer.chaliceOfTheBloodGod && calamityPlayer.chaliceBleedoutBuffer > 0.0 && !calamityPlayer.bloomStone)
				{
					float num = 0.5f * (float)player.GetHealLife(potion, quickHeal: true);
					calamityPlayer.chaliceBleedoutBuffer -= num;
					if (!Main.dedServ)
					{
						string key = $"(+{num})";
						CombatText.NewText(new Rectangle((int)player.position.X + 4, (int)player.position.Y - 3, player.width - 4, player.height - 4), ChaliceOfTheBloodGod.BleedoutBufferDamageTextColor, Language.GetTextValue(key), dramatic: false, dot: true);
					}
				}
			}
		});
	}

	private static bool AllowNegativeCheckMana(orig_CheckMana_int_bool_bool orig, Player self, int amount, bool pay, bool blockQuickMana)
	{
		if (self.Calamity().ChaosStone)
		{
			if (pay)
			{
				self.statMana -= amount;
			}
			if (self.statMana < -self.statManaMax2)
			{
				self.statMana = -self.statManaMax2;
			}
			return true;
		}
		return orig.Invoke(self, amount, pay, blockQuickMana);
	}

	private static bool AllowNegativeCheckMana(orig_CheckMana_Item_int_bool_bool orig, Player self, Item item, int amount, bool pay, bool blockQuickMana)
	{
		if (self.Calamity().ChaosStone)
		{
			if (pay)
			{
				self.statMana -= item.mana;
			}
			if (self.statMana < -self.statManaMax2)
			{
				self.statMana = -self.statManaMax2;
			}
			return true;
		}
		return orig.Invoke(self, item, amount, pay, blockQuickMana);
	}

	private static void UseCoolFireCursorEffect(orig_DrawCursor orig, Vector2 bonus, bool smart)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		if (Main.gameMenu || Main.mapFullscreen || !player.Calamity().blazingCursorVisuals)
		{
			orig.Invoke(bonus, smart);
			return;
		}
		if (player.dead)
		{
			Main.ClearSmartInteract();
			Main.TileInteractionLX = (Main.TileInteractionHX = (Main.TileInteractionLY = (Main.TileInteractionHY = -1)));
		}
		Color cursorColor = Color.Lerp(Color.DarkRed, Color.OrangeRed, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 7.4f) * 0.5f + 0.5f) * 1.9f;
		Vector2 baseDrawPosition = Main.MouseScreen + bonus;
		if (!PlayerInput.UsingGamepad)
		{
			int cursorIndex = smart.ToInt();
			Color desaturatedCursorColor = cursorColor;
			((Color)(ref desaturatedCursorColor)).R = (byte)(((Color)(ref desaturatedCursorColor)).R / 5);
			((Color)(ref desaturatedCursorColor)).G = (byte)(((Color)(ref desaturatedCursorColor)).G / 5);
			((Color)(ref desaturatedCursorColor)).B = (byte)(((Color)(ref desaturatedCursorColor)).B / 5);
			((Color)(ref desaturatedCursorColor)).A = (byte)(((Color)(ref desaturatedCursorColor)).A / 2);
			Vector2 drawPosition = baseDrawPosition;
			Vector2 desaturatedDrawPosition = drawPosition + Vector2.One;
			if (!Main.mapFullscreen)
			{
				int size = 370;
				FluidFieldManager.AdjustSizeRelativeToGraphicsQuality(ref size);
				float scale = MathHelper.Max((float)Main.screenWidth, (float)Main.screenHeight) / (float)size;
				ref FluidField calamityFireDrawer = ref player.Calamity().CalamityFireDrawer;
				ref Vector2 firePosition = ref player.Calamity().FireDrawerPosition;
				if (calamityFireDrawer == null || calamityFireDrawer.Size != size)
				{
					calamityFireDrawer = FluidFieldManager.CreateField(size, scale, 0.1f, 50f, 0.992f);
				}
				firePosition = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
				int x = (int)((drawPosition.X - firePosition.X) / calamityFireDrawer.Scale);
				int y = (int)((drawPosition.Y - firePosition.Y) / calamityFireDrawer.Scale);
				int horizontalArea = (int)Math.Ceiling(8f / calamityFireDrawer.Scale);
				int verticalArea = (int)Math.Ceiling(8f / calamityFireDrawer.Scale);
				calamityFireDrawer.ShouldUpdate = player.miscCounter % 2 == 0;
				calamityFireDrawer.UpdateAction = delegate
				{
					//IL_0000: Unknown result type (might be due to invalid IL or missing references)
					//IL_0005: Unknown result type (might be due to invalid IL or missing references)
					//IL_0028: Unknown result type (might be due to invalid IL or missing references)
					//IL_002d: Unknown result type (might be due to invalid IL or missing references)
					//IL_0040: Unknown result type (might be due to invalid IL or missing references)
					//IL_0061: Unknown result type (might be due to invalid IL or missing references)
					//IL_006b: Unknown result type (might be due to invalid IL or missing references)
					//IL_0070: Unknown result type (might be due to invalid IL or missing references)
					//IL_0088: Unknown result type (might be due to invalid IL or missing references)
					//IL_0089: Unknown result type (might be due to invalid IL or missing references)
					//IL_0093: Unknown result type (might be due to invalid IL or missing references)
					//IL_0098: Unknown result type (might be due to invalid IL or missing references)
					//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
					//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
					//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
					//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
					//IL_0110: Unknown result type (might be due to invalid IL or missing references)
					//IL_0115: Unknown result type (might be due to invalid IL or missing references)
					//IL_011a: Unknown result type (might be due to invalid IL or missing references)
					//IL_011c: Unknown result type (might be due to invalid IL or missing references)
					//IL_0121: Unknown result type (might be due to invalid IL or missing references)
					//IL_0126: Unknown result type (might be due to invalid IL or missing references)
					//IL_0130: Unknown result type (might be due to invalid IL or missing references)
					//IL_0136: Unknown result type (might be due to invalid IL or missing references)
					//IL_0138: Unknown result type (might be due to invalid IL or missing references)
					//IL_0142: Unknown result type (might be due to invalid IL or missing references)
					//IL_0147: Unknown result type (might be due to invalid IL or missing references)
					//IL_0149: Unknown result type (might be due to invalid IL or missing references)
					//IL_015f: Unknown result type (might be due to invalid IL or missing references)
					//IL_0164: Unknown result type (might be due to invalid IL or missing references)
					//IL_0109: Unknown result type (might be due to invalid IL or missing references)
					//IL_010e: Unknown result type (might be due to invalid IL or missing references)
					//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
					//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
					//IL_01af: Unknown result type (might be due to invalid IL or missing references)
					Color val = Color.Lerp(Color.Red, Color.Orange, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.5f + 0.5f);
					if (player.hasRainbowCursor)
					{
						val = Color.Lerp(val, Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.97f % 1f, 1f, 0.6f), 0.75f);
					}
					if (player.Calamity().CalamityFireDyeShader != null)
					{
						val = Color.Lerp(val, Color.White, 0.75f);
					}
					float num = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6.7f) * 0.99f;
					int num2 = size / 2;
					for (int i = -horizontalArea; i <= horizontalArea; i++)
					{
						float num3 = num;
						num3 += (float)i / (float)horizontalArea * 0.34f;
						Vector2 val2 = Main.MouseWorld - UIManagementSystem.PreviousMouseWorld;
						if (((Vector2)(ref val2)).Length() < 64f)
						{
							num3 *= 0.5f;
							val2 = Vector2.Zero;
						}
						UIManagementSystem.PreviousMouseWorld = Main.MouseWorld;
						val2 = val2.SafeNormalize(-Vector2.UnitY).RotatedBy(num3) * 3f;
						val2 *= Main.rand.NextFloat(0.9f, 1.1f);
						for (int j = -verticalArea; j <= verticalArea; j++)
						{
							player.Calamity().CalamityFireDrawer.CreateSource(x + num2 + i, y + num2 + j, 1f, val, (i == 0 && j == 0) ? val2 : Vector2.Zero);
						}
					}
				};
				calamityFireDrawer.Draw(firePosition, needsToCallEnd: true, Main.UIScaleMatrix, Main.UIScaleMatrix, delegate(RenderTarget2D output)
				{
					//IL_0018: Unknown result type (might be due to invalid IL or missing references)
					//IL_001d: Unknown result type (might be due to invalid IL or missing references)
					player.Calamity().CalamityFireDyeShader?.Apply(null, new DrawData((Texture2D)(object)output, Vector2.Zero, Color.White));
				});
			}
			Main.spriteBatch.Draw(TextureAssets.Cursors[cursorIndex].Value, drawPosition, (Rectangle?)null, desaturatedCursorColor, 0f, Vector2.Zero, Main.cursorScale, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.UIScaleMatrix);
			GameShaders.Misc["CalamityMod:FireMouse"].UseColor(Color.Red);
			GameShaders.Misc["CalamityMod:FireMouse"].UseSecondaryColor(Color.Lerp(Color.Red, Color.Orange, 0.75f));
			GameShaders.Misc["CalamityMod:FireMouse"].Apply();
			Main.spriteBatch.Draw(TextureAssets.Cursors[cursorIndex].Value, desaturatedDrawPosition, (Rectangle?)null, cursorColor, 0f, Vector2.Zero, Main.cursorScale * 1.075f, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.EffectMatrix);
		}
		else if ((!Main.LocalPlayer.dead || Main.LocalPlayer.ghost || Main.gameMenu) && !PlayerInput.InvisibleGamepadInMenus)
		{
			if (smart && (!UILinkPointNavigator.Available || PlayerInput.InBuildingMode))
			{
				cursorColor = Color.White * Main.GamepadCursorAlpha;
				int frameX = 0;
				Texture2D smartCursorTexture = TextureAssets.Cursors[13].Value;
				Rectangle frame = smartCursorTexture.Frame(2, 1, frameX);
				Main.spriteBatch.Draw(smartCursorTexture, baseDrawPosition, (Rectangle?)frame, cursorColor, 0f, frame.Size() * 0.5f, Main.cursorScale, (SpriteEffects)0, 0f);
			}
			else
			{
				cursorColor = Color.White;
				Texture2D crosshairTexture = TextureAssets.Cursors[15].Value;
				Main.spriteBatch.Draw(crosshairTexture, baseDrawPosition, (Rectangle?)null, cursorColor, 0f, crosshairTexture.Size() * 0.5f, Main.cursorScale, (SpriteEffects)0, 0f);
			}
		}
	}

	private static void CustomDoDrawChanges(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCallOrCallvirt<Main>(i, "DrawInfernoRings")
		}) || !cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall<ScreenObstruction>(i, "Draw")
		}))
		{
			return;
		}
		cursor.EmitDelegate<Action>((Action)delegate
		{
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ModProjectile is IAdditiveDrawer additiveDrawer)
				{
					additiveDrawer.AdditiveDraw(Main.spriteBatch);
				}
			}
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.ModNPC is IAdditiveDrawer additiveDrawer2)
				{
					additiveDrawer2.AdditiveDraw(Main.spriteBatch);
				}
			}
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		});
	}

	private static void DrawFusableParticles(orig_SortDrawCacheWorms orig, Main self)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		DeathAshParticle.DrawAll();
		if (Main.LocalPlayer.dye.Any((Item dyeItem) => dyeItem.type == ModContent.ItemType<ProfanedMoonlightDye>()))
		{
			Main.LocalPlayer.Calamity().ProfanedMoonlightAuroraDrawer?.Draw(Main.LocalPlayer.Center - Main.screenPosition, needsToCallEnd: false, Main.GameViewMatrix.TransformationMatrix, Matrix.Identity);
		}
		orig.Invoke(self);
	}

	private static void DisableFlashesWithPhotosensitivityConfig(orig_RequestLight orig, float light, Vector2 spot)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (!CalamityClientConfig.Instance.Photosensitivity)
		{
			orig.Invoke(light, spot);
		}
	}

	private static void AddTwinklersToStatue(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		for (int i = 0; i < 2; i++)
		{
			if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
			{
				(Instruction val) => ILPatternMatchingExt.MatchCall(val, typeof(Utils), "SelectRandom")
			}))
			{
				LogFailure("Add Twinkler to Firefly Statue", "Could not move to the SelectRandom method.");
				return;
			}
		}
		cursor.EmitDelegate<Func<short[], short[]>>((Func<short[], short[]>)delegate(short[] arr)
		{
			Array.Resize(ref arr, arr.Length + 1);
			arr[^1] = (short)ModContent.NPCType<Twinkler>();
			return arr;
		});
	}

	private static void MakeTaxCollectorUseful(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall<Item>(i, "buyPrice")
		}))
		{
			LogFailure("Tax Collector Money Boosts", "Could not locate the amount of money to collect per town NPC.");
			return;
		}
		cursor.Emit(OpCodes.Pop);
		cursor.Emit<CalamityGlobalTownNPC>(OpCodes.Call, "get_TotalTaxesPerNPC");
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall<Item>(i, "buyPrice")
		}))
		{
			LogFailure("Tax Collector Money Boosts", "Could not locate the maximum amount of money to collect.");
			return;
		}
		cursor.Emit(OpCodes.Pop);
		cursor.Emit<CalamityGlobalTownNPC>(OpCodes.Call, "get_TaxesToCollectLimit");
	}

	private static void ClearTilePings(orig_Draw orig, TileDrawing self, bool solidLayer, bool forRenderTargets, bool intoRenderTargets, int waterStyleOverride)
	{
		if (Lighting.UpdateEveryFrame)
		{
			if (!solidLayer)
			{
				TilePingerSystem.ClearTiles();
			}
		}
		else if (Lighting.Mode == LightMode.White)
		{
			if (solidLayer)
			{
				TilePingerSystem.ClearTiles();
			}
		}
		else
		{
			TilePingerSystem.ClearTiles(solidLayer);
		}
		orig.Invoke(self, solidLayer, forRenderTargets, intoRenderTargets, waterStyleOverride);
	}

	private static void CustomGrappleMovementCheck(orig_GrappleMovement orig, Player self)
	{
		if (!self.GetModPlayer<WulfrumPackPlayer>().GrappleMovementDisabled)
		{
			orig.Invoke(self);
		}
	}

	private static void CustomGrapplePreDefaultMovement(orig_UpdatePettingAnimal orig, Player self)
	{
		orig.Invoke(self);
		WulfrumPackPlayer mp = self.GetModPlayer<WulfrumPackPlayer>();
		mp.hookCache = -1;
		if (!self.tongued && self.grappling[0] >= 0 && mp.GrappleMovementDisabled && Main.projectile[self.grappling[0]].type == ModContent.ProjectileType<WulfrumHook>())
		{
			mp.hookCache = self.grappling[0];
			self.grappling[0] = -1;
			self.grapCount = 0;
		}
	}

	private static void CustomGrapplePreStepUp(orig_SlopeDownMovement orig, Player self)
	{
		orig.Invoke(self);
		WulfrumPackPlayer mp = self.GetModPlayer<WulfrumPackPlayer>();
		if (self.grappling[0] >= 0 && mp.GrappleMovementDisabled && Main.projectile[self.grappling[0]].type == ModContent.ProjectileType<WulfrumHook>())
		{
			mp.hookCache = self.grappling[0];
			self.grappling[0] = -1;
			self.grapCount = 0;
		}
	}

	private static void CustomGrapplePostFrame(orig_PlayerFrame orig, Player self)
	{
		orig.Invoke(self);
		WulfrumPackPlayer mp = self.GetModPlayer<WulfrumPackPlayer>();
		if (mp.hookCache > -1)
		{
			self.grappling[0] = mp.hookCache;
			self.grapCount = 1;
		}
		mp.hookCache = -1;
	}

	private static void ScopesRequireVisibilityToZoom(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStfld<Player>(i, "scope")
		}))
		{
			LogFailure("Scopes Require Visibility to Zoom", "Could not locate where player is set to have a scope equipped.");
			return;
		}
		cursor.Emit(OpCodes.Pop);
		cursor.Emit(OpCodes.Ldarg_2);
		cursor.EmitDelegate<Func<bool, bool>>((Func<bool, bool>)((bool x) => !x));
	}

	internal static void GetDifficultyOverride(orig_GetDifficulty orig, AWorldListItem self, out string expertText, out Color gameModeColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self, ref expertText, ref gameModeColor);
		string difficultyText = expertText;
		Color difficultyColor = gameModeColor;
		if (difficultyColor == Main.creativeModeColor)
		{
			return;
		}
		for (int i = WorldSelectionDifficultySystem.WorldDifficulties.Count - 1; i >= 0; i--)
		{
			WorldSelectionDifficultySystem.WorldDifficulty d = WorldSelectionDifficultySystem.WorldDifficulties[i];
			if (d.function(self))
			{
				difficultyText = d.name;
				difficultyColor = d.color;
				break;
			}
		}
		expertText = difficultyText;
		gameModeColor = difficultyColor;
	}

	public static void ShimmerEffectEdits(orig_GetShimmered orig, Item self)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (self.type == ModContent.ItemType<PlaguedContainmentBrick>())
		{
			if (NPC.downedGolemBoss)
			{
				orig.Invoke(self);
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				int nanodroidType = (Main.rand.NextBool() ? ModContent.NPCType<NanodroidPlagueGreen>() : ModContent.NPCType<NanodroidPlagueRed>());
				NPC droids = NPC.NewNPCDirect(self.GetSource_FromThis(), (int)self.Center.X, (int)self.Center.Y, nanodroidType);
				droids.velocity = -self.velocity.RotatedByRandom(0.15707963705062866) * 2f;
				droids.netUpdate = true;
				droids.shimmerTransparency = 1f;
				NetMessage.SendData(146, -1, -1, null, 2, droids.whoAmI);
			}
			self.TurnToAir();
			self.shimmerWet = true;
			self.wet = true;
			self.velocity *= 0.1f;
			if (Main.netMode == 0)
			{
				Item.ShimmerEffect(self.Center);
				return;
			}
			NetMessage.SendData(146, -1, -1, null, 0, (int)self.Center.X, (int)self.Center.Y);
			NetMessage.SendData(145, -1, -1, null, self.whoAmI, 1f);
		}
		else
		{
			orig.Invoke(self);
		}
	}

	public static void TPOverride(orig_Teleport orig, Player self, Vector2 newPos, int Style = 0, int extraInfo = 0)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		Tile t = CalamityUtils.ParanoidTileRetrieval(newPos.ToTileCoordinates().X, newPos.ToTileCoordinates().Y);
		if (Style == 2)
		{
			if (CalamityTileSets.IsAbyssWall[t.WallType])
			{
				bool canSpawn = false;
				int teleportStartX = 100;
				int teleportRangeX = Main.maxTilesX - 200;
				int teleportStartY = 100;
				int underworldLayer = Main.UnderworldLayer;
				Vector2 newerPos = self.CheckForGoodTeleportationSpot(ref canSpawn, teleportStartX, teleportRangeX, teleportStartY, underworldLayer, new Player.RandomTeleportationAttemptSettings
				{
					avoidLava = true,
					avoidHurtTiles = true,
					maximumFallDistanceFromOrignalPoint = 100,
					attemptsBeforeGivingUp = 1000
				});
				orig.Invoke(self, newerPos, Style, extraInfo);
			}
			else
			{
				orig.Invoke(self, newPos, Style, extraInfo);
			}
		}
		else
		{
			orig.Invoke(self, newPos, Style, extraInfo);
			if (Style == 8 && CalamityTileSets.IsAbyssWall[t.WallType])
			{
				self.AddBuff(88, 2);
			}
		}
	}

	private static void GlowMaskTileRender(orig_DrawSingleTile orig, TileDrawing self, TileDrawInfo drawData, bool solidLayer, int waterStyleOverride, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self, drawData, solidLayer, waterStyleOverride, screenPosition, screenOffset, tileX, tileY);
		ushort type = drawData.typeCache;
		if (type >= GlowMaskTile.LookupLength)
		{
			return;
		}
		GlowMaskTile glowMaskTile = GlowMaskTile.InstanceLookup[type];
		if (glowMaskTile == null || !TileDrawing.IsVisible(drawData.tileCache))
		{
			return;
		}
		FramedMaskTexture glowMask = glowMaskTile.GlowMask;
		int xPos = drawData.tileFrameX + drawData.addFrX;
		int yPos = drawData.tileFrameY + drawData.addFrY;
		if (!glowMask.HasContentInFramePos(xPos, yPos))
		{
			return;
		}
		ref Tile tileCache = ref drawData.tileCache;
		int colType = tileCache.TileColor;
		Color drawColor = glowMaskTile.GetGlowMaskColor(tileX, tileY, drawData);
		if (glowMaskTile.GlowMaskAffectedByLight)
		{
			Color tileLight = drawData.tileLight;
			if (((Color)(ref tileLight)).R > ((Color)(ref drawColor)).R)
			{
				((Color)(ref drawColor)).R = ((Color)(ref tileLight)).R;
			}
			if (((Color)(ref tileLight)).G > ((Color)(ref drawColor)).G)
			{
				((Color)(ref drawColor)).G = ((Color)(ref tileLight)).G;
			}
			if (((Color)(ref tileLight)).B > ((Color)(ref drawColor)).B)
			{
				((Color)(ref drawColor)).B = ((Color)(ref tileLight)).B;
			}
		}
		drawColor = (Color)(glowMaskTile.GlowMaskPaintInteraction switch
		{
			GlowMaskTile.PaintColorTint.OnlyByDeepPaint => CalamityUtils.ApplyPaint(colType, drawColor), 
			GlowMaskTile.PaintColorTint.ByEveryPaint => CalamityUtils.ApplyPaint(colType, drawColor, deepPaintOnly: false), 
			_ => drawColor, 
		});
		if (!glowMaskTile.GlowMaskCanBeCulled || ((Color)(ref drawColor)).R > 1 || ((Color)(ref drawColor)).G > 1 || ((Color)(ref drawColor)).B > 1)
		{
			((Color)(ref drawColor)).A = byte.MaxValue;
			if (Main.tileSolid[type])
			{
				Rectangle drawRect = default(Rectangle);
				((Rectangle)(ref drawRect))._002Ector(xPos, yPos, 16, 16);
				TileFramingSystem.SlopedGlowmask(in tileCache, tileX, tileY, glowMask.Texture, drawRect, drawColor, default(Vector2));
			}
			else
			{
				Vector2 drawPos = new Vector2((float)(tileX * 16), (float)(tileY * 16)) - screenPosition + screenOffset;
				Rectangle drawRect2 = default(Rectangle);
				((Rectangle)(ref drawRect2))._002Ector(xPos, yPos, 16, 16);
				Main.spriteBatch.Draw(glowMask.Texture, drawPos, (Rectangle?)drawRect2, drawColor, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
		}
	}

	public static void AllowCannonJellyfishUse(orig_PlaceThing_CannonBall orig, Player self)
	{
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		if (self.HeldItem.type == ModContent.ItemType<BabyCannonballJellyfishItem>())
		{
			bool veryLongRangeCheck = self.position.X / 16f - (float)Player.tileRangeX - (float)self.HeldItem.tileBoost - (float)self.blockRange <= (float)Player.tileTargetX && (self.position.X + (float)self.width) / 16f + (float)Player.tileRangeX + (float)self.HeldItem.tileBoost - 1f + (float)self.blockRange >= (float)Player.tileTargetX && self.position.Y / 16f - (float)Player.tileRangeY - (float)self.HeldItem.tileBoost - (float)self.blockRange <= (float)Player.tileTargetY && (self.position.Y + (float)self.height) / 16f + (float)Player.tileRangeY + (float)self.HeldItem.tileBoost - 2f + (float)self.blockRange >= (float)Player.tileTargetY;
			int targX = Player.tileTargetX;
			int targY = Player.tileTargetY;
			Tile t = CalamityUtils.ParanoidTileRetrieval(targX, targY);
			int cannonType = t.TileFrameX / 72;
			if ((t.TileType == 209 && cannonType == 0 && self.ItemTimeIsZero && self.controlUseItem) & veryLongRangeCheck)
			{
				self.cursorItemIconEnabled = true;
				self.cursorItemIconID = ModContent.ItemType<BabyCannonballJellyfishItem>();
				self.HeldItem.makeNPC = -1;
				int tileX = t.TileFrameX / 18;
				int angle = 0;
				while (tileX >= 4)
				{
					tileX -= 4;
				}
				tileX = targX - tileX;
				int tileY;
				for (tileY = t.TileFrameY / 18; tileY >= 3; tileY -= 3)
				{
					angle++;
				}
				tileY = targY - tileY;
				self.ApplyItemTime(self.HeldItem);
				float speedX = 0f;
				float speedY = 0f;
				if (angle == 0)
				{
					speedX = 10f;
					speedY = 0f;
				}
				if (angle == 1)
				{
					speedX = 7.5f;
					speedY = -2.5f;
				}
				if (angle == 2)
				{
					speedX = 5f;
					speedY = -5f;
				}
				if (angle == 3)
				{
					speedX = 2.75f;
					speedY = -6f;
				}
				if (angle == 4)
				{
					speedX = 0f;
					speedY = -10f;
				}
				if (angle == 5)
				{
					speedX = -2.75f;
					speedY = -6f;
				}
				if (angle == 6)
				{
					speedX = -5f;
					speedY = -5f;
				}
				if (angle == 7)
				{
					speedX = -7.5f;
					speedY = -2.5f;
				}
				if (angle == 8)
				{
					speedX = -10f;
					speedY = 0f;
				}
				Vector2 spawnPosition = default(Vector2);
				((Vector2)(ref spawnPosition))._002Ector((float)((tileX + 2) * 16), (float)((tileY + 2) * 16));
				int jellyfishb = Projectile.NewProjectile(new EntitySource_TileInteraction(self, tileX, tileY), spawnPosition.X, spawnPosition.Y, speedX, speedY, ModContent.ProjectileType<BabyCannonballProjectile>(), self.HeldItem.damage, 8f, self.whoAmI);
				Main.projectile[jellyfishb].originatedFromActivableTile = true;
				SoundEngine.PlaySound(in SoundID.Item95, spawnPosition);
			}
			else
			{
				self.HeldItem.makeNPC = ModContent.NPCType<BabyCannonballJellyfish>();
			}
		}
		else
		{
			orig.Invoke(self);
		}
	}

	private static void DrawTreeTrunkAndCactusGlowMask(orig_DrawBasicTile orig, TileDrawing self, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData, Rectangle normalTileRect, Vector2 normalTilePosition)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self, screenPosition, screenOffset, tileX, tileY, drawData, normalTileRect, normalTilePosition);
		switch (drawData.typeCache)
		{
		case 80:
		{
			short frameX = drawData.tileFrameX;
			short frameY = drawData.tileFrameY;
			int xPos3 = drawData.tileFrameX + drawData.addFrX;
			int yPos3 = drawData.tileFrameY + drawData.addFrY;
			WorldGen.GetCactusType(tileX, tileY, frameX, frameY, out var sandType);
			if (PlantLoader.Get<ModCactus>(80, sandType) is GlowMaskCactus glowCacti)
			{
				Vector2 drawPos2 = new Vector2((float)(tileX * 16), (float)(tileY * 16)) - screenPosition + screenOffset;
				Rectangle drawRect3 = default(Rectangle);
				((Rectangle)(ref drawRect3))._002Ector(xPos3, yPos3, 16, 16);
				Texture2D texture3 = ((drawData.tileFrameX == 204 || drawData.tileFrameY == 202) ? glowCacti.GetFruitGlowTexture() : glowCacti.GetGlowTexture())?.Value;
				if (texture3 != null)
				{
					Main.spriteBatch.Draw(texture3, drawPos2, (Rectangle?)drawRect3, glowCacti.GetGlowColor(tileX, tileY), 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			break;
		}
		case 5:
		{
			int xPos2 = drawData.tileFrameX + drawData.addFrX;
			int yPos2 = drawData.tileFrameY + drawData.addFrY;
			WorldGen.GetTreeBottom(tileX, tileY, out var groundX2, out var groundY2);
			if (PlantLoader.Get<ModTree>(5, Main.tile[groundX2, groundY2].TileType) is GlowMaskTree glowTree2)
			{
				Vector2 drawPos = new Vector2((float)(tileX * 16), (float)(tileY * 16)) - screenPosition + screenOffset;
				Rectangle drawRect2 = default(Rectangle);
				((Rectangle)(ref drawRect2))._002Ector(xPos2, yPos2, 16, 16);
				Texture2D texture2 = glowTree2.GetGlowTexture()?.Value;
				if (texture2 != null)
				{
					Main.spriteBatch.Draw(texture2, drawPos, (Rectangle?)drawRect2, glowTree2.GetGlowColor(tileX, tileY), 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			break;
		}
		case 323:
		{
			int xPos = drawData.tileFrameX + drawData.addFrX;
			int yPos = drawData.tileFrameY + drawData.addFrY;
			WorldGen.GetTreeBottom(tileX, tileY, out var groundX, out var groundY);
			if (PlantLoader.Get<ModPalmTree>(323, Main.tile[groundX, groundY].TileType) is GlowMaskPalmTree glowTree)
			{
				Rectangle drawRect = default(Rectangle);
				((Rectangle)(ref drawRect))._002Ector(xPos, yPos, 16, 16);
				Texture2D texture = glowTree.GetGlowTexture()?.Value;
				if (texture != null)
				{
					Main.spriteBatch.Draw(texture, normalTilePosition, (Rectangle?)drawRect, glowTree.GetGlowColor(tileX, tileY), 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			break;
		}
		}
	}

	private static void DisableCullingForTreeAndCactus(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		int visibleFlagIndex = 0;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[4]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdloc(x, ref visibleFlagIndex),
			(Instruction x) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchLdarg(x, ref num);
			},
			(Instruction x) =>
			{
				FieldReference val = default(FieldReference);
				return ILPatternMatchingExt.MatchLdfld(x, ref val);
			},
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt<TileDrawing>(x, "IsVisible")
		}))
		{
			LogFailure("Disable Tree And Cactus Culling", "Unable to Locate IsVisible Call");
			return;
		}
		if (!cursor.TryGotoPrev((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction x) =>
			{
				ILLabel val = default(ILLabel);
				return ILPatternMatchingExt.MatchBrfalse(x, ref val);
			}
		}))
		{
			LogFailure("Disable Tree And Cactus Culling", "Unable to Locate Brfalse.s");
			return;
		}
		cursor.EmitLdarg1();
		cursor.EmitDelegate<Func<TileDrawInfo, bool>>((Func<TileDrawInfo, bool>)delegate(TileDrawInfo drawInfo)
		{
			ushort typeCache = drawInfo.typeCache;
			return typeCache == 80 || typeCache == 5 || typeCache == 323;
		});
		cursor.EmitLdloc(visibleFlagIndex);
		cursor.EmitOr();
		cursor.EmitStloc(visibleFlagIndex);
	}

	private static void DrawTreeGlowMask(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdfld<Point>(x, "X")
		}))
		{
			LogFailure("GlowMask Tree Rendering", "Unable to Locate Ldfld for Point::X");
			return;
		}
		int xLocaIdx = default(int);
		if (!ILPatternMatchingExt.MatchStloc(cursor.Next, ref xLocaIdx))
		{
			LogFailure("GlowMask Tree Rendering", "Unable to Locate Stloc Index for Point::X");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdfld<Point>(x, "Y")
		}))
		{
			LogFailure("GlowMask Tree Rendering", "Unable to Locate Ldfld for Point::Y");
			return;
		}
		int yLocaIdx = default(int);
		if (!ILPatternMatchingExt.MatchStloc(cursor.Next, ref yLocaIdx))
		{
			LogFailure("GlowMask Tree Rendering", "Unable to Locate Stloc Index for Point::Y");
			return;
		}
		ApplyTreeGlowMaskSubParts(cursor, "GlowMaskTree-Top", "GetTreeTopTexture", xLocaIdx, yLocaIdx, delegate(GlowMaskTree glowMaskTree, int tileX, int tileY)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			return new GlowMaskPlantDrawInfo
			{
				Texture = glowMaskTree.GetTopGlowTextures().Value,
				Color = glowMaskTree.GetGlowColor(tileX, tileY)
			};
		});
		ApplyTreeGlowMaskSubParts(cursor, "GlowMaskTree-R", "GetTreeBranchTexture", xLocaIdx, yLocaIdx, delegate(GlowMaskTree glowMaskTree, int tileX, int tileY)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			return new GlowMaskPlantDrawInfo
			{
				Texture = glowMaskTree.GetBranchGlowTextures().Value,
				Color = glowMaskTree.GetGlowColor(tileX, tileY)
			};
		});
		ApplyTreeGlowMaskSubParts(cursor, "GlowMaskTree-L", "GetTreeBranchTexture", xLocaIdx, yLocaIdx, delegate(GlowMaskTree glowMaskTree, int tileX, int tileY)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			return new GlowMaskPlantDrawInfo
			{
				Texture = glowMaskTree.GetBranchGlowTextures().Value,
				Color = glowMaskTree.GetGlowColor(tileX, tileY)
			};
		});
		ApplyPalmTreeGlowMaskSubParts(cursor, "GlowMaskPalmTree-Top", "GetTreeTopTexture", xLocaIdx, yLocaIdx, delegate(GlowMaskPalmTree glowMaskPalmTree, int tileX, int tileY)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			return new GlowMaskPlantDrawInfo
			{
				Texture = (WorldGen.IsPalmOasisTree(tileX) ? glowMaskPalmTree.GetOasisTopGlowTextures().Value : glowMaskPalmTree.GetTopGlowTextures().Value),
				Color = glowMaskPalmTree.GetGlowColor(tileX, tileY)
			};
		});
	}

	private static void ApplyTreeGlowMaskSubParts<PlantType>(ILCursor cursor, string debugSubpartName, string getTextureMethodName, int xLocaIdx, int yLocaIdx, Func<PlantType, int, int, GlowMaskPlantDrawInfo?> onPlantDrawn) where PlantType : IPlant
	{
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt<TileDrawing>(x, getTextureMethodName)
		}))
		{
			LogFailure("GlowMask Tree Rendering", $"Could not locate First {getTextureMethodName} call ({debugSubpartName})");
			return;
		}
		if (!cursor.TryGotoPrev(new Func<Instruction, bool>[2]
		{
			(Instruction x) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchLdcI4(x, ref num) && num == 14;
			},
			(Instruction x) =>
			{
				ILLabel val = default(ILLabel);
				return ILPatternMatchingExt.MatchBneUn(x, ref val);
			}
		}))
		{
			LogFailure("GlowMask Tree Rendering", "Could not locate Bne.Un for TreeStyleIndex (" + debugSubpartName + ")");
			return;
		}
		int treeStyleLocaIdx = default(int);
		if (!ILPatternMatchingExt.MatchLdloc(cursor.Prev, ref treeStyleLocaIdx))
		{
			LogFailure("GlowMask Tree Rendering", "Prev Instruction is not a LdLoc. Cannot locate TreeStyleIndex (" + debugSubpartName + ")");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt<SpriteBatch>(x, "Draw")
		}))
		{
			LogFailure("GlowMask Tree Rendering", "Could not locate DrawCall (" + debugSubpartName + ")");
			return;
		}
		cursor.EmitLdloc(treeStyleLocaIdx);
		cursor.EmitLdloc(xLocaIdx);
		cursor.EmitLdloc(yLocaIdx);
		cursor.EmitDelegate<Action<SpriteBatch, Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float, int, int, int>>((Action<SpriteBatch, Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float, int, int, int>)delegate(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, int style, int tileX, int tileY)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
			if (style >= 100)
			{
				int growsOnTileID = style - 100;
				if (PlantLoader.Get<ModTree>(5, growsOnTileID) is PlantType arg)
				{
					GlowMaskPlantDrawInfo? glowMaskPlantDrawInfo = (onPlantDrawn?.Invoke(arg, tileX, tileY)).GetValueOrDefault();
					if (glowMaskPlantDrawInfo.HasValue)
					{
						GlowMaskPlantDrawInfo value = glowMaskPlantDrawInfo.Value;
						spriteBatch.Draw(value.Texture, position, sourceRectangle, value.Color, rotation, origin, scale, effects, layerDepth);
					}
				}
			}
		});
		cursor.Next.OpCode = OpCodes.Nop;
	}

	private static void ApplyPalmTreeGlowMaskSubParts<PlantType>(ILCursor cursor, string debugSubpartName, string getTextureMethodName, int xLocaIdx, int yLocaIdx, Func<PlantType, int, int, GlowMaskPlantDrawInfo?> onPlantDrawn)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt<TileDrawing>(x, getTextureMethodName)
		}))
		{
			LogFailure("GlowMask Tree Rendering", $"Could not locate First {getTextureMethodName} call ({debugSubpartName})");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt<SpriteBatch>(x, "Draw")
		}))
		{
			LogFailure("GlowMask Tree Rendering", "Could not locate DrawCall (" + debugSubpartName + ")");
			return;
		}
		cursor.EmitLdloc(xLocaIdx);
		cursor.EmitLdloc(yLocaIdx);
		cursor.EmitDelegate<Action<SpriteBatch, Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float, int, int>>((Action<SpriteBatch, Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float, int, int>)delegate(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, int tileX, int tileY)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
			WorldGen.GetTreeBottom(tileX, tileY, out var x, out var y);
			ushort tileType = Main.tile[x, y].TileType;
			if (PlantLoader.Get<ModPalmTree>(323, tileType) is PlantType arg)
			{
				GlowMaskPlantDrawInfo? glowMaskPlantDrawInfo = (onPlantDrawn?.Invoke(arg, tileX, tileY)).GetValueOrDefault();
				if (glowMaskPlantDrawInfo.HasValue)
				{
					GlowMaskPlantDrawInfo value = glowMaskPlantDrawInfo.Value;
					spriteBatch.Draw(value.Texture, position, sourceRectangle, value.Color, rotation, origin, scale, effects, layerDepth);
				}
			}
		});
		cursor.Next.OpCode = OpCodes.Nop;
	}

	public static bool MasterModeCelestialOnionCheck(orig_IsItemSlotUnlockedAndUsable orig, Player self, int slot)
	{
		if ((slot == 9 || slot == 19) && self.Calamity().extraAccessoryML && !Main.gameMenu)
		{
			return true;
		}
		return orig.Invoke(self, slot);
	}

	public static void AllowHooksToGrabArenabox(orig_AI_007_GrapplingHooks orig, Projectile self)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		if (Main.player[self.owner].dead || Main.player[self.owner].stoned || Main.player[self.owner].webbed || Main.player[self.owner].frozen)
		{
			self.Kill();
			return;
		}
		bool intersectingWall = false;
		if (self.Calamity().arenaBox != null)
		{
			ArenaWallSystem.Box box = self.Calamity().arenaBox;
			if (ArenaWallSystem.ActiveBoxes.Contains(box))
			{
				self.Center = box.TopLeft + box.Size * self.Calamity().arenaBoxPosition;
			}
			else
			{
				self.Calamity().arenaBox = null;
			}
		}
		if (ArenaWallSystem.ActiveBoxes.Count > 0)
		{
			foreach (ArenaWallSystem.Box box2 in ArenaWallSystem.ActiveBoxes)
			{
				if (Vector2PointCollision(box2.TopLeft, box2.Size, self.Center) || !Vector2PointCollision(box2.TopLeft - new Vector2(box2.borderThickness), box2.Size + new Vector2(box2.borderThickness) * 2f, self.Center))
				{
					continue;
				}
				if (self.ai[0] == 0f)
				{
					if (Main.myPlayer == self.owner)
					{
						if (self.type == 935)
						{
							Main.player[self.owner].DoQueenSlimeHookTeleport(self.Center);
						}
						NetMessage.SendData(13, -1, -1, null, self.owner);
					}
					SoundEngine.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy with
					{
						Volume = 0.75f
					}, self.Center);
					self.ai[0] = 2f;
					self.velocity = Vector2.Zero;
					self.Calamity().arenaBoxPosition = new Vector2(Utils.Remap(self.Center.X, box2.TopLeft.X, box2.BottomRight.X, 0f, 1f, clamped: false), Utils.Remap(self.Center.Y, box2.TopLeft.Y, box2.BottomRight.Y, 0f, 1f, clamped: false));
					self.Calamity().arenaBox = box2;
				}
				if (self.ai[0] == 2f)
				{
					self.rotation = self.DirectionFrom(Main.player[self.owner].Center).ToRotation() + (float)Math.PI / 2f;
					intersectingWall = true;
					if (Main.player[self.owner].grapCount < 10)
					{
						Main.player[self.owner].grappling[Main.player[self.owner].grapCount] = self.whoAmI;
						Main.player[self.owner].grapCount++;
					}
				}
			}
		}
		if (!intersectingWall)
		{
			orig.Invoke(self);
		}
		static bool Vector2PointCollision(Vector2 position, Vector2 size, Vector2 point)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			if (point.X >= position.X && point.X <= position.X + size.X && point.Y >= position.Y)
			{
				return point.Y <= position.Y + size.Y;
			}
			return false;
		}
	}

	private static bool ArenaCollision_Vector2_int_int_bool(orig_SolidCollision_Vector2_int_int_bool orig, Vector2 Position, int Width, int Height, bool acceptTopSurfaces)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (ArenaWallSystem.ActiveBoxes.Count > 0)
		{
			foreach (ArenaWallSystem.Box activeBox in ArenaWallSystem.ActiveBoxes)
			{
				if (activeBox.Vector2PairInWall(Position, new Vector2((float)Width, (float)Height)))
				{
					return true;
				}
			}
		}
		return orig.Invoke(Position, Width, Height, acceptTopSurfaces);
	}

	private static bool ArenaCollision_Vector2_int_int(orig_SolidCollision_Vector2_int_int orig, Vector2 Position, int Width, int Height)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (ArenaWallSystem.ActiveBoxes.Count > 0)
		{
			foreach (ArenaWallSystem.Box activeBox in ArenaWallSystem.ActiveBoxes)
			{
				if (activeBox.Vector2PairInWall(Position, new Vector2((float)Width, (float)Height)))
				{
					return true;
				}
			}
		}
		return orig.Invoke(Position, Width, Height);
	}

	private static Vector2 ArenaCollision_TileCollision(orig_TileCollision orig, Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough, bool fall2, int gravDir)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Velocity = orig.Invoke(Position, Velocity, Width, Height, fallThrough, fall2, gravDir);
		if (ArenaWallSystem.ActiveBoxes.Count > 0 && Velocity != Vector2.Zero)
		{
			foreach (ArenaWallSystem.Box item in ArenaWallSystem.ActiveBoxes)
			{
				Vector2 oldVel = Velocity;
				if (item.InnerEffect(Position, new Vector2((float)Width, (float)Height)))
				{
					Velocity = ArenaCollisionLogic(item, Position, Width, Height, Velocity);
				}
				Vector2 val = oldVel - Velocity;
				((Vector2)(ref val)).Length();
			}
		}
		return Velocity;
	}

	private static Vector2 ArenaCollisionLogic(ArenaWallSystem.Box box, Vector2 Position, int Width, int Height, Vector2 Velocity)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 originalVelocity = Velocity;
		Vector2 originalTopLeft = Position;
		Vector2 originalBottomRight = Position + new Vector2((float)Height, (float)Width);
		Position += originalVelocity;
		if (Position.X < box.TopLeft.X)
		{
			Velocity.X = box.TopLeft.X - originalTopLeft.X;
		}
		if (Position.X + (float)Width > box.BottomRight.X)
		{
			Velocity.X = box.BottomRight.X - originalBottomRight.X;
		}
		if (Position.Y < box.TopLeft.Y)
		{
			Velocity.Y = box.TopLeft.Y - originalTopLeft.Y;
		}
		if (Position.Y + (float)Height > box.BottomRight.Y)
		{
			Velocity.Y = box.BottomRight.Y - originalBottomRight.Y;
		}
		return Velocity;
	}

	private static void FixHeldProjectileBlendState(orig_DrawHeldProj orig, PlayerDrawSet drawinfo, Projectile proj)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(drawinfo, proj);
		SamplerState sampler = ((drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.fullRotation != 0f) ? LegacyPlayerRenderer.MountedSamplerState : Main.DefaultSamplerState);
		try
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, sampler, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		catch
		{
			if (!HasLoggedHeldProjectileBlendStateCatch)
			{
				LogFailure("FixHeldProjectileBlendState", "The spritebatch was not left properly by another mod! The game will now most likely crash.");
			}
			HasLoggedHeldProjectileBlendStateCatch = true;
		}
	}

	private static void FixTruffleWormFishing(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		il.Method.Body.Variables.Add(new VariableDefinition(il.Module.TypeSystem.Boolean));
		byte truffleWormUsed = (byte)(il.Method.Body.Variables.Count - 1);
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.Stloc_S, truffleWormUsed);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			delegate(Instruction i)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return ILPatternMatchingExt.Match(i, OpCodes.Beq_S);
			}
		}))
		{
			LogFailure("FixTruffleWormFishing", "Could not locate beq.s before Player.ItemCheck_CheckFishingBobber_PickAndConsumeBait.");
			return;
		}
		ILLabel loopEnd = il.DefineLabel();
		cursor.Emit(OpCodes.Ldloc_S, truffleWormUsed);
		cursor.Emit(OpCodes.Brtrue_S, (object)loopEnd);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall<Player>(i, "ItemCheck_CheckFishingBobber_PickAndConsumeBait")
		}))
		{
			LogFailure("FixTruffleWormFishing", "Could not locate the call to Player.ItemCheck_CheckFishingBobber_PickAndConsumeBait.");
			return;
		}
		cursor.Emit(OpCodes.Ldloc_S, (byte)4);
		cursor.Emit(OpCodes.Ldc_I4, 2673);
		cursor.Emit(OpCodes.Ceq);
		cursor.Emit(OpCodes.Stloc_S, truffleWormUsed);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdloc0(i)
		}))
		{
			LogFailure("FixTruffleWormFishing", "Could not find the end of the loop.");
		}
		else
		{
			cursor.MarkLabel(loopEnd);
		}
	}

	private static void EnsureCheckDeadOnSegments(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[3]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<NPC>(i, "realLife"),
			(Instruction i) => ILPatternMatchingExt.MatchLdelemRef(i),
			(Instruction i) => ILPatternMatchingExt.MatchCallOrCallvirt<NPC>(i, "checkDead")
		}))
		{
			LogFailure("EnsureCheckDeadOnSegments", "Could not locate the checkDead instruction sets");
			return;
		}
		cursor.EmitLdarg0();
		cursor.EmitDelegate<Action<NPC>>((Action<NPC>)delegate(NPC npc)
		{
			if (npc.life <= 0 && CalamityNPCSets.DoCheckDeadRegardlessRealLife[npc.type])
			{
				NPCLoader.CheckDead(npc);
			}
		});
	}

	private static void DecreaseSandstormWindSpeedRequirement(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0.6f)
		}))
		{
			LogFailure("Decrease Sandstorm Wind Speed Requirement", "Could not locate the wind speed variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 0.2f);
	}

	private static void RelaxPrefixRequirements(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall(i, "System.Math", "Round")
		}))
		{
			LogFailure("Prefix Requirements", "Could not locate the damage Math.Round call.");
			return;
		}
		ILLabel passesDamageCheck = null;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchBneUn(i, ref passesDamageCheck)
		}))
		{
			LogFailure("Prefix Requirements", "Could not locate damage prefix failure branch.");
			return;
		}
		cursor.Emit(OpCodes.Br_S, (object)passesDamageCheck);
		ILLabel passesUseTimeCheck = null;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchBneUn(i, ref passesUseTimeCheck)
		}))
		{
			LogFailure("Prefix Requirements", "Could not locate use time rounding equality branch.");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.EmitDelegate<Func<Item, bool>>((Func<Item, bool>)((Item i) => i.useAnimation >= 2 && i.useAnimation <= 5));
		cursor.Emit(OpCodes.Brtrue_S, (object)passesUseTimeCheck);
		ILLabel passesManaCheck = null;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchBneUn(i, ref passesManaCheck)
		}))
		{
			LogFailure("Prefix Requirements", "Could not locate mana prefix failure branch.");
			return;
		}
		cursor.Emit(OpCodes.Br_S, (object)passesManaCheck);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Item>(i, "knockBack")
		}))
		{
			LogFailure("Prefix Requirements", "Could not locate knockback load instruction.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0f)
		}))
		{
			LogFailure("Prefix Requirements", "Could not locate zero knockback comparison constant.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, -1000000f);
	}

	private static void PreventBossSlimeRainSpawns(orig_SlimeRainSpawns orig, int plr)
	{
		if (!CalamityServerConfig.Instance.BossZen || !Main.player[plr].Calamity().isNearbyBoss)
		{
			orig.Invoke(plr);
		}
	}

	private static void RemoveExpertBrainRandomDebuffs(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchCall<Main>(i, "get_expertMode")
		}))
		{
			LogFailure("Remove Expert Brain Random Debuffs", "Could not locate the Expert Mode check.");
			return;
		}
		cursor.Emit(OpCodes.Ldc_I4_0);
		cursor.Emit(OpCodes.And);
	}

	private static void PreventLavaSlimeLavaDrop(orig_HitEffect_HitInfo orig, NPC self, NPC.HitInfo hit)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (self.type != 59 || !CalamityServerConfig.Instance.RemoveLavaDropsFromLavaSlimes)
		{
			orig.Invoke(self, hit);
			return;
		}
		int i = (int)(self.Center.X / 16f);
		int tileY = (int)(self.Center.Y / 16f);
		Tile tile = Framing.GetTileSafely(i, tileY);
		if (tile.LiquidAmount != 0)
		{
			orig.Invoke(self, hit);
			return;
		}
		byte originalAmount = tile.LiquidAmount;
		tile.LiquidAmount = byte.MaxValue;
		orig.Invoke(self, hit);
		tile.LiquidAmount = originalAmount;
	}

	private static void LetDetonatingBubblesTakeDamage(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcR8(i, 0.0)
		}))
		{
			LogFailure("Let Detonating Bubbles Take Damage in Death", "Could not move after the NPC type check.");
			return;
		}
		ILLabel label = il.DefineLabel();
		cursor.Emit(OpCodes.Ldsfld, typeof(CalamityWorld).GetField("death"));
		cursor.Emit(OpCodes.Brtrue, (object)label);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStfld<NPC>(i, "dontTakeDamage")
		}))
		{
			LogFailure("Let Detonating Bubbles Take Damage in Death", "Could not move to after the Detonating Bubble logic.");
		}
		else if (!cursor.TryGotoNext((MoveType)1, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdarg0(i)
		}))
		{
			LogFailure("Let Detonating Bubbles Take Damage in Death", "Could not move to after the Detonating Bubble logic.");
		}
		else
		{
			cursor.MarkLabel(label);
		}
	}

	private static void PunchCameraUsesScreenshakeConfig(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchStloc2(i)
		}))
		{
			LogFailure("Make PunchCameraModifier Affected by Screenshake Config", "Could not move to the location to inject code.");
			return;
		}
		cursor.Emit(OpCodes.Ldloc_1);
		cursor.EmitDelegate<Func<float>>((Func<float>)(() => CalamityClientConfig.Instance.ScreenshakePower));
		cursor.Emit(OpCodes.Mul);
		cursor.Emit(OpCodes.Stloc_1);
	}

	private static void MakeMeteoriteExplodable(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		ILLabel label = null;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchBeq(i, ref label)
		}))
		{
			LogFailure("Make Meteorite Explodable", "Could not locate the branching instruction.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 37)
		}))
		{
			LogFailure("Make Meteorite Explodable", "Could not locate the Meteorite Tile ID variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 76);
	}

	private static void BloodMoonsRequire200MaxLife(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction c) => ILPatternMatchingExt.MatchLdsfld<Main>(c, "moonPhase")
		}))
		{
			LogFailure("Make Blood Moons Require 200 Max Life", "Could not locate the moon phase check.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction c) => ILPatternMatchingExt.MatchCallOrCallvirt<Player>(c, "get_ConsumedLifeCrystals")
		}))
		{
			LogFailure("Make Blood Moons Require 200 Max Life", "Could not locate the Life Crystal check.");
			return;
		}
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 1)
		}))
		{
			LogFailure("Make Blood Moons Require 200 Max Life", "Could not locate the Life Crystal requirement.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 4);
	}

	private static void PreventFossilShattering(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 404)
		}))
		{
			LogFailure("Prevent Fossil Shattering", "Could not locate the Desert Fossil Tile ID variable.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 40000);
	}

	private static int RemoveHellforgePickaxeRequirement(orig_GetPickaxeDamage orig, Player self, int x, int y, int pickPower, int hitBufferIndex, Tile tileTarget)
	{
		if (tileTarget.TileType == 77)
		{
			pickPower = 65;
		}
		return orig.Invoke(self, x, y, pickPower, hitBufferIndex, tileTarget);
	}

	private static void FlailsNoLongerAffectedByPlayerVelocity(orig_AI_015_Flails orig, Projectile self)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self);
		if (self.ai[0] == 1f && self.ai[1] == 0f)
		{
			self.velocity -= Main.player[self.owner].velocity;
		}
	}

	private static void PreventUFODismountInWater(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		for (int i = 0; i < 3; i++)
		{
			if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
			{
				(Instruction val) => ILPatternMatchingExt.MatchCallvirt<Mount>(val, "Dismount")
			}))
			{
				LogFailure("Prevent UFO Dismounting in Water", "Could not reach the Dismount instruction.");
				return;
			}
		}
		if (!cursor.TryGotoPrev((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction val) => ILPatternMatchingExt.MatchLdsfld<Main>(val, "myPlayer")
		}))
		{
			LogFailure("Prevent UFO Dismounting in Water", "Could not locate the myPlayer check.");
			return;
		}
		cursor.EmitPop();
		cursor.Emit(OpCodes.Ldc_I4, int.MaxValue);
	}

	private static void ColorBlightedGel(orig_ModifyItemDropFromNPC orig, NPC npc, int itemIndex)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(npc, itemIndex);
		Item item = Main.item[itemIndex];
		int type = item.type;
		bool colorWasChanged = false;
		if (type == ModContent.ItemType<BlightedGel>() && npc.type == ModContent.NPCType<CrimulanBlightSlime>())
		{
			item.color = new Color(1f, 0f, 0.16f, 0.6f);
			colorWasChanged = true;
		}
		if (type == 319 && npc.type == ModContent.NPCType<Mauler>())
		{
			item.color = new Color(151, 115, 57, 255);
			colorWasChanged = true;
		}
		if (colorWasChanged)
		{
			NetMessage.SendData(88, -1, -1, null, itemIndex, 1f);
		}
	}

	private static void AddMoreGuaranteedAnglerRewards(orig_GetAnglerReward_MainReward orig, Player self, List<Item> rewardItems, IEntitySource source, int questsDone, float rarityReduction, int questItemType, ref GetItemSettings anglerRewardSettings)
	{
		Item item = new Item();
		item.type = 0;
		List<int> checkingList = new List<int>();
		bool botheredRollingForADrop;
		switch (questsDone)
		{
		case 3:
		{
			checkingList.Add(2373);
			if (self.DropAnglerAccByMissing(checkingList, 1f, out botheredRollingForADrop, out var highTest))
			{
				item.SetDefaults(highTest);
			}
			break;
		}
		case 6:
		{
			checkingList.Add(3120);
			if (self.DropAnglerAccByMissing(checkingList, 1f, out botheredRollingForADrop, out var fishGuide))
			{
				item.SetDefaults(fishGuide);
			}
			break;
		}
		case 10:
		{
			Item vest = new Item(2368);
			rewardItems.Add(vest);
			Item pants = new Item(2369);
			rewardItems.Add(pants);
			break;
		}
		case 11:
		{
			checkingList.Add(3037);
			if (self.DropAnglerAccByMissing(checkingList, 1f, out botheredRollingForADrop, out var radio))
			{
				item.SetDefaults(radio);
			}
			break;
		}
		case 14:
		{
			checkingList.Add(3096);
			if (self.DropAnglerAccByMissing(checkingList, 1f, out botheredRollingForADrop, out var sextant))
			{
				item.SetDefaults(sextant);
			}
			break;
		}
		case 15:
		{
			checkingList.Add(2375);
			if (self.DropAnglerAccByMissing(checkingList, 1f, out botheredRollingForADrop, out var tackle))
			{
				item.SetDefaults(tackle);
			}
			break;
		}
		case 20:
		{
			checkingList.Add(2374);
			if (self.DropAnglerAccByMissing(checkingList, 1f, out botheredRollingForADrop, out var earring))
			{
				item.SetDefaults(earring);
			}
			break;
		}
		case 26:
			item.SetDefaults(3064);
			break;
		case 28:
			item.SetDefaults(3183);
			break;
		}
		if (item.type > 0)
		{
			rewardItems.Add(item);
		}
		else
		{
			orig.Invoke(self, rewardItems, source, questsDone, rarityReduction, questItemType, ref anglerRewardSettings);
		}
	}

	private static void ImproveAnglerBaitReward(orig_GetAnglerReward_Bait orig, Player self, List<Item> rewardItems, IEntitySource source, int questsDone, float rarityReduction, ref GetItemSettings anglerRewardSettings)
	{
		Item bait = new Item();
		if (Main.rand.NextBool((int)(15f * rarityReduction)))
		{
			bait.SetDefaults(ModContent.ItemType<GrandMarquisBait>());
		}
		else if (Main.rand.NextBool((int)(10f * rarityReduction)))
		{
			bait.SetDefaults(2676);
		}
		else if (Main.rand.NextBool((int)(5f * rarityReduction)))
		{
			bait.SetDefaults(2675);
		}
		else
		{
			bait.SetDefaults(2674);
		}
		bait.stack = 2;
		if (Main.rand.Next(10) <= questsDone)
		{
			bait.stack++;
		}
		if (Main.rand.Next(20) <= questsDone)
		{
			bait.stack++;
		}
		if (Main.rand.Next(30) <= questsDone)
		{
			bait.stack++;
		}
		if (Main.rand.Next(40) <= questsDone)
		{
			bait.stack++;
		}
		if (Main.rand.Next(50) <= questsDone)
		{
			bait.stack++;
		}
		rewardItems.Add(bait);
	}

	private static void ImproveAnglerMoneyReward(orig_GetAnglerReward_Money orig, Player self, List<Item> rewardItems, IEntitySource source, int questsDone, float rarityReduction, ref GetItemSettings anglerRewardSettings)
	{
		int moneyDrop = (questsDone + 70) / 2;
		moneyDrop = (int)((float)moneyDrop * Main.rand.NextFloat(1f, 2f));
		moneyDrop = (int)((float)moneyDrop * 1.5f);
		if (Main.hardMode)
		{
			moneyDrop *= 2;
		}
		if (Main.expertMode)
		{
			moneyDrop *= 2;
		}
		if (moneyDrop > 1000)
		{
			moneyDrop = 1000;
		}
		if (moneyDrop >= 100)
		{
			int goldDrop = moneyDrop / 100;
			Item gold = new Item();
			gold.SetDefaults(73);
			gold.stack = goldDrop;
			rewardItems.Add(gold);
		}
		int silverDrop = moneyDrop % 100;
		Item silver = new Item();
		silver.SetDefaults(72);
		silver.stack = silverDrop;
		rewardItems.Add(silver);
	}

	private static void UseVisibleThroughWaterMapTile(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		ILCursor c = new ILCursor(il);
		if (!c.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCall<Tilemap>(x, "get_Item")
		}))
		{
			LogFailure("Use VisibleThroughWater Map Tile", "Could not locate call to Terraria.Map.TileMap::get_Item.");
			return;
		}
		int tileIndex = -1;
		if (!c.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchStloc(x, ref tileIndex)
		}) || tileIndex == -1)
		{
			LogFailure("Use VisibleThroughWater Map Tile", "Could not determine the local variable index tile is pushed to.");
			return;
		}
		if (!c.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCall<Tile>(x, "liquidType")
		}))
		{
			LogFailure("Use VisibleThroughWater Map Tile", "Could not locate call to Terraria.Tile::liquidType.");
			return;
		}
		int liquidTypeIndex = -1;
		if (!c.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchStloc(x, ref liquidTypeIndex)
		}) || liquidTypeIndex == -1)
		{
			LogFailure("Use VisibleThroughWater Map Tile", "Could not determine the local variable index liquidType is pushed to.");
			return;
		}
		int relativeMapTypeIndex = -1;
		if (!c.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchStloc(x, ref relativeMapTypeIndex)
		}) || relativeMapTypeIndex == -1)
		{
			LogFailure("Use VisibleThroughWater Map Tile", "Could not determine the local variable index of the relative map type.");
			return;
		}
		c.Emit(OpCodes.Ldloc_0);
		c.Emit(OpCodes.Ldloc, relativeMapTypeIndex);
		c.Emit(OpCodes.Ldloc, liquidTypeIndex);
		c.EmitDelegate<Func<Tile, int, int, int>>((Func<Tile, int, int, int>)delegate(Tile tile, int relativeMapType, int liquidType)
		{
			if (liquidType != 0)
			{
				return relativeMapType;
			}
			return (WallLoader.GetWall(tile.WallType) is IVisibleThroughWater visibleThroughWater) ? visibleThroughWater.WaterMapEntry : relativeMapType;
		});
		c.Emit(OpCodes.Stloc, relativeMapTypeIndex);
	}

	private static void MakeMagmaStoneFireGauntletDustToggleable(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "magmaStone")
		}))
		{
			LogFailure("Make Magma Stone & Fire Gauntlet Dust Toggleable", "Could not locate the Magma Stone variable.");
			return;
		}
		cursor.Emit(OpCodes.Ldarg_0);
		cursor.EmitDelegate<Func<Player, bool>>(MagmaStoneVisualsEnabled);
		cursor.Emit(OpCodes.And);
	}

	private static void MakeMagmaStoneFireGauntletProjectileDustToggleable(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Player>(i, "magmaStone")
		}))
		{
			LogFailure("Make Magma Stone & Fire Gauntlet Projectile Dust Toggleable", "Could not locate the magma stone variable.");
			return;
		}
		cursor.Emit(OpCodes.Ldloc_0);
		cursor.EmitDelegate<Func<Player, bool>>(MagmaStoneVisualsEnabled);
		cursor.Emit(OpCodes.And);
	}

	private static void RemovePowerCellPlanteraLock(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdsfld<NPC>(i, "downedPlantBoss")
		}))
		{
			LogFailure("Remove Power Cell Plantera Lock", "Could not locate the downed Plantera bool.");
			return;
		}
		cursor.EmitPop();
		cursor.Emit(OpCodes.Ldc_I4_1);
	}

	private static bool RemoveUseLocks(orig_ItemCheck_CheckCanUse orig, Player self, Item sItem)
	{
		if (sItem.type == 3601)
		{
			if (!NPC.AnyNPCs(398))
			{
				return !BossRushEvent.BossRushActive;
			}
			return false;
		}
		if (sItem.type == 2767)
		{
			if (Main.dayTime && !Main.eclipse)
			{
				if (!Main.hardMode && !NPC.downedMechBossAny)
				{
					return NPC.downedPlantBoss;
				}
				return true;
			}
			return false;
		}
		return orig.Invoke(self, sItem);
	}

	private static void ApplyCelestialSigilChanges(orig_ItemCheck_UseEventItems orig, Player self, Item sItem)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (self.ItemTimeIsZero && self.itemAnimation > 0 && sItem.type == 3601)
		{
			if (!NPC.AnyNPCs(398) && !BossRushEvent.BossRushActive)
			{
				SoundEngine.PlaySound(in SoundID.Roar, self.Center);
				self.ApplyItemTime(sItem);
				if (Main.netMode != 1)
				{
					NPC.SpawnOnPlayer(self.whoAmI, 398);
				}
				else
				{
					NetMessage.SendData(61, -1, -1, null, self.whoAmI, 398f);
				}
			}
		}
		else
		{
			orig.Invoke(self, sItem);
		}
	}

	private static void RemoveDamageConditionFromRadar(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		Func<Instruction, bool>[] searchFor = new Func<Instruction, bool>[3]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdfld<NPC>(x, "damage"),
			(Instruction x) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchLdcI4(x, ref num) && num == 0;
			},
			(Instruction x) =>
			{
				ILLabel val = default(ILLabel);
				return ILPatternMatchingExt.MatchBle(x, ref val);
			}
		};
		if (!cursor.TryGotoNext((MoveType)2, searchFor))
		{
			LogFailure("Radar Condition", "Unable to locate condition for NPC.damage > 0");
			return;
		}
		cursor.Prev.OpCode = OpCodes.Nop;
		cursor.EmitPop();
		cursor.EmitPop();
	}

	private static void DelayGravity(orig_UpdateControlHolds orig, Player Player)
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer cplay = Player.Calamity();
		if (CalamityKeybinds.SwitchGravityHotkey.GetAssignedKeysOrEmpty().Count != 0 && (Player.gravControl || Player.gravControl2) && !Player.mount.Active)
		{
			if (Player.controlUp && Player.releaseUp)
			{
				Player.gravDir *= -1f;
			}
			if (CalamityKeybinds.SwitchGravityHotkey.JustPressed)
			{
				Player.gravDir *= -1f;
				Player.fallStart = (int)(Player.position.Y / 16f);
				Player.jump = 0;
				SoundEngine.PlaySound(in SoundID.Item8, Player.position);
			}
			if (Player.forcedGravity > 0)
			{
				Player.gravDir = -1f;
			}
		}
		if (cplay.justChangedGravity)
		{
			Player.gravDir = cplay.oldGravDir;
		}
		cplay.justChangedGravity = cplay.oldGravDir != Player.gravDir;
		cplay.oldGravDir = Player.gravDir;
		if (Main.netMode != 2 && !Main.gameMenu && CalamityClientConfig.Instance.DisableGravityScreenSwap)
		{
			if (Player.gravDir == -1f)
			{
				if (!Filters.Scene["CalamityMod:FlipScreen"].IsActive())
				{
					Filters.Scene.Activate("CalamityMod:FlipScreen", default(Vector2));
					Filters.Scene["CalamityMod:FlipScreen"].Opacity = 1f;
				}
			}
			else if (Filters.Scene["CalamityMod:FlipScreen"].IsActive())
			{
				Filters.Scene["CalamityMod:FlipScreen"].Opacity = 0f;
				Filters.Scene.Deactivate("CalamityMod:FlipScreen");
			}
		}
		if (cplay.justChangedGravity)
		{
			Player.gravDir *= -1f;
		}
		orig.Invoke(Player);
	}

	private static void GravityMouse(orig_SetZoom_MouseInWorld orig)
	{
		orig.Invoke();
		if (!Main.gameMenu && Filters.Scene["CalamityMod:FlipScreen"].IsActive())
		{
			int center = Main.screenHeight / 2;
			Main.mouseY = center - (Main.mouseY - center);
		}
	}

	private static void UI_Unflip_Start(orig_DrawPlayerChatBubbles orig, Main self)
	{
		if (!Main.gameMenu && (Filters.Scene["CalamityMod:FlipScreen"].IsActive() || Main.LocalPlayer.Calamity().justChangedGravity))
		{
			Main.LocalPlayer.Calamity().tempGravDir = Main.LocalPlayer.gravDir;
			Main.LocalPlayer.gravDir = 1f;
		}
		orig.Invoke(self);
	}

	private static void UI_Unflip_End(orig_DrawInterface orig, Main self, GameTime gameTime)
	{
		orig.Invoke(self, gameTime);
		if (!Main.gameMenu && Filters.Scene["CalamityMod:FlipScreen"].IsActive())
		{
			Main.LocalPlayer.gravDir = Main.LocalPlayer.Calamity().tempGravDir;
		}
	}

	[Obsolete("This function serves no purpose and is included in the Calamity source code for historic value.", true)]
	private static void StoreWindGrid(orig_Update orig, TileDrawing self)
	{
		orig.Invoke(self);
		if (Windgrid == null)
		{
			Windgrid = typeof(TileDrawing).GetField("_windGrid", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self) as WindGrid;
		}
	}

	private static void BlockLivingTreesNearOcean(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		ILCursor val = new ILCursor(il);
		val.Emit(OpCodes.Ldarg_0);
		val.EmitDelegate<Func<int, int>>((Func<int, int>)((int x) => Utils.Clamp(x, 560, Main.maxTilesX - 560)));
		val.Emit(OpCodes.Starg, 0);
	}

	private static void PreventSmashAltarCode(orig_SmashAltar orig, int i, int j)
	{
		if (!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework)
		{
			orig.Invoke(i, j);
		}
	}

	private static void AdjustChlorophyteSpawnRate(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 300)
		}))
		{
			LogFailure("Chlorophyte Spread Rate", "Could not locate the update chance.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 150);
	}

	private static void AdjustChlorophyteSpawnLimits(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 40)
		}))
		{
			LogFailure("Chlorophyte Spread Limit", "Could not locate the lower limit.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 60);
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[1]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 130)
		}))
		{
			LogFailure("Chlorophyte Spread Limit", "Could not locate the upper limit.");
			return;
		}
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_I4, 200);
	}

	private static void ChangeDefaultWorldSize(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		ILCursor c = new ILCursor(il);
		if (!c.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) =>
			{
				ILLabel val = default(ILLabel);
				return ILPatternMatchingExt.MatchBr(x, ref val);
			}
		}))
		{
			LogFailure("Change Default World Size", "Could not match start of branched for loop.");
			return;
		}
		if (!c.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdcI4(x, 0)
		}))
		{
			LogFailure("Change Default World Size", "Could not match '0' indicating WorldSizeId.Small.");
			return;
		}
		c.Emit(OpCodes.Pop);
		c.Emit(OpCodes.Ldc_I4_2);
		if (!c.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchRet(x)
		}))
		{
			LogFailure("Change Default World Size", "Could not match end of method.");
			return;
		}
		c.Emit(OpCodes.Ldarg_0);
		c.Emit(OpCodes.Ldc_I4_2);
		c.Emit<UIWorldCreation>(OpCodes.Stfld, "_optionSize");
		c.Emit(OpCodes.Ldarg_0);
		c.Emit<UIWorldCreation>(OpCodes.Call, "UpdatePreviewPlate");
	}

	private static void SwapSmallDescriptionKey(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ILCursor c = new ILCursor(il);
		if (!c.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdstr(x, "UI.WorldDescriptionSizeSmall")
		}))
		{
			LogFailure("Change Small World Description", "Could not match string \"UI.WorldDescriptionSizeSmall\".");
			return;
		}
		c.Emit(OpCodes.Pop);
		c.Emit(OpCodes.Ldstr, "Mods.CalamityMod.UI.SmallWorldWarning");
	}

	private static void LimitDungeonEntranceXPosition(orig_MakeDungeon orig, int x, int y)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		x = Utils.Clamp(x, DungeonBaseXLimit, Main.maxTilesX - DungeonBaseXLimit);
		if (WorldUtils.Find(new Point(x, y), Searches.Chain(new Searches.Down(9001), new Terraria.WorldBuilding.Conditions.IsSolid()), out var result))
		{
			y = result.Y - 10;
		}
		orig.Invoke(x, y);
	}

	private static void LimitDungeonHallsXPosition(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor c = new ILCursor(il);
		if (!c.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdarg0(x)
		}))
		{
			LogFailure("Limit Dungeon Hall X Positions", "Could not match the load of argument 0.");
			return;
		}
		c.EmitDelegate<Func<int, int>>((Func<int, int>)((int x) => Utils.Clamp(x, DungeonHallXLimit, Main.maxTilesX - DungeonHallXLimit)));
	}
}
