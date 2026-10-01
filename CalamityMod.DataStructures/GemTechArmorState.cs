using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.GemTech;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class GemTechArmorState
{
	public int OwnerIndex;

	public int RedGemRegenerationCountdown;

	public int YellowGemRegenerationCountdown;

	public int GreenGemRegenerationCountdown;

	public int BlueGemRegenerationCountdown;

	public int PurpleGemRegenerationCountdown;

	public int PinkGemRegenerationCountdown;

	public int MeleeCrystalCountdown;

	public int LifeRegenBonusCountdown;

	public int MultiWeaponLifeRegenBonusCountdown;

	public GemTechArmorGemType GemThatShouldBeLost = GemTechArmorGemType.Base;

	public GemTechArmorGemType? PreviouslyUsedGem;

	public Player Owner => Main.player[OwnerIndex];

	public bool HasInvalidOwner
	{
		get
		{
			if (OwnerIndex >= 0 && OwnerIndex < 255)
			{
				return !Owner.active;
			}
			return true;
		}
	}

	public bool IsRedGemActive => RedGemRegenerationCountdown <= 0;

	public bool IsYellowGemActive => YellowGemRegenerationCountdown <= 0;

	public bool IsGreenGemActive => GreenGemRegenerationCountdown <= 0;

	public bool IsBlueGemActive => BlueGemRegenerationCountdown <= 0;

	public bool IsPurpleGemActive => PurpleGemRegenerationCountdown <= 0;

	public bool IsPinkGemActive => PinkGemRegenerationCountdown <= 0;

	public bool AllGemsActive
	{
		get
		{
			if (IsRedGemActive && IsYellowGemActive && IsGreenGemActive && IsBlueGemActive && IsPurpleGemActive)
			{
				return IsPinkGemActive;
			}
			return false;
		}
	}

	public GemTechArmorState(int ownerIndex)
	{
		OwnerIndex = ownerIndex;
	}

	public bool GemIsActive(GemTechArmorGemType gemType)
	{
		return gemType switch
		{
			GemTechArmorGemType.Melee => IsYellowGemActive, 
			GemTechArmorGemType.Ranged => IsGreenGemActive, 
			GemTechArmorGemType.Magic => IsPurpleGemActive, 
			GemTechArmorGemType.Summoner => IsBlueGemActive, 
			GemTechArmorGemType.Rogue => IsRedGemActive, 
			GemTechArmorGemType.Base => IsPinkGemActive, 
			_ => false, 
		};
	}

	public void Update()
	{
		if (!Owner.HeldItem.IsAir && !Owner.HeldItem.accessory)
		{
			if (Owner.HeldItem.CountsAsClass<MeleeDamageClass>())
			{
				GemThatShouldBeLost = GemTechArmorGemType.Melee;
			}
			if (Owner.HeldItem.CountsAsClass<RangedDamageClass>())
			{
				GemThatShouldBeLost = GemTechArmorGemType.Ranged;
			}
			if (Owner.HeldItem.CountsAsClass<MagicDamageClass>())
			{
				GemThatShouldBeLost = GemTechArmorGemType.Magic;
			}
			if (Owner.HeldItem.CountsAsClass<SummonDamageClass>())
			{
				GemThatShouldBeLost = GemTechArmorGemType.Summoner;
			}
			if (Owner.HeldItem.CountsAsClass<ThrowingDamageClass>())
			{
				GemThatShouldBeLost = GemTechArmorGemType.Rogue;
			}
		}
		if (!GemIsActive(GemThatShouldBeLost))
		{
			GemThatShouldBeLost = GemTechArmorGemType.Base;
		}
		if (RedGemRegenerationCountdown > 0)
		{
			RedGemRegenerationCountdown--;
			if (RedGemRegenerationCountdown == 0)
			{
				CreateRegenerationEffect(GemTechArmorGemType.Rogue);
			}
		}
		if (YellowGemRegenerationCountdown > 0)
		{
			YellowGemRegenerationCountdown--;
			if (YellowGemRegenerationCountdown == 0)
			{
				CreateRegenerationEffect(GemTechArmorGemType.Melee);
			}
		}
		if (GreenGemRegenerationCountdown > 0)
		{
			GreenGemRegenerationCountdown--;
			if (GreenGemRegenerationCountdown == 0)
			{
				CreateRegenerationEffect(GemTechArmorGemType.Ranged);
			}
		}
		if (BlueGemRegenerationCountdown > 0)
		{
			BlueGemRegenerationCountdown--;
			if (BlueGemRegenerationCountdown == 0)
			{
				CreateRegenerationEffect(GemTechArmorGemType.Summoner);
			}
		}
		if (PurpleGemRegenerationCountdown > 0)
		{
			PurpleGemRegenerationCountdown--;
			if (PurpleGemRegenerationCountdown == 0)
			{
				CreateRegenerationEffect(GemTechArmorGemType.Magic);
			}
		}
		if (PinkGemRegenerationCountdown > 0)
		{
			PinkGemRegenerationCountdown--;
			if (PinkGemRegenerationCountdown == 0)
			{
				CreateRegenerationEffect(GemTechArmorGemType.Base);
			}
		}
		if (MeleeCrystalCountdown > 0)
		{
			MeleeCrystalCountdown--;
			if (Owner.HeldItem.IsTrueMelee())
			{
				MeleeCrystalCountdown--;
			}
			if (MeleeCrystalCountdown < 0)
			{
				MeleeCrystalCountdown = 0;
			}
		}
		if (LifeRegenBonusCountdown > 0)
		{
			LifeRegenBonusCountdown--;
		}
		if (MultiWeaponLifeRegenBonusCountdown > 0)
		{
			MultiWeaponLifeRegenBonusCountdown = 0;
		}
	}

	public void MeleeOnHitEffects(NPC target)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.Calamity().GemTechSet && IsYellowGemActive && Main.myPlayer == OwnerIndex && MeleeCrystalCountdown <= 0)
		{
			int damage = (int)Owner.GetTotalDamage<MeleeDamageClass>().ApplyTo(825f);
			for (int i = 0; i < 14; i++)
			{
				Vector2 shootVelocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(0.5f, 3.25f);
				Projectile.NewProjectile(Owner.GetSource_OnHit(target), target.Center, shootVelocity, ModContent.ProjectileType<GemTechYellowShard>(), damage, 0f, OwnerIndex);
			}
			MeleeCrystalCountdown = 330;
		}
	}

	public void RangedOnHitEffects(NPC target, int hitDamage)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		bool hasReachedProjCountLimit = Owner.ownedProjectileCounts[ModContent.ProjectileType<GemTechGreenFlechette>()] > 8;
		if (!((!Owner.Calamity().GemTechSet || !IsGreenGemActive || Main.myPlayer != OwnerIndex) | hasReachedProjCountLimit))
		{
			int damage = CalamityUtils.DamageSoftCap((int)((float)hitDamage * 0.32f), 400);
			Vector2 spawnPosition = Owner.Center + Main.rand.NextVector2Circular(Owner.width, Owner.height) * 1.35f;
			Vector2 shootVelocity = (target.Center - spawnPosition) * 0.04f;
			if (((Vector2)(ref shootVelocity)).Length() < 6f)
			{
				shootVelocity = shootVelocity.SafeNormalize(Vector2.UnitY) * 6f;
			}
			spawnPosition -= shootVelocity.SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(15f, 50f);
			Projectile.NewProjectile(Owner.GetSource_OnHit(target), spawnPosition, shootVelocity, ModContent.ProjectileType<GemTechGreenFlechette>(), damage, 0f, OwnerIndex);
		}
	}

	public void OnItemUseEffects(Item item)
	{
		if (!item.IsAir && item.damage > 0)
		{
			LifeRegenBonusCountdown = 480;
			GemTechArmorGemType? usedGemType = null;
			if (item.CountsAsClass<MeleeDamageClass>())
			{
				usedGemType = GemTechArmorGemType.Melee;
			}
			if (item.CountsAsClass<RangedDamageClass>())
			{
				usedGemType = GemTechArmorGemType.Ranged;
			}
			if (item.CountsAsClass<MagicDamageClass>())
			{
				usedGemType = GemTechArmorGemType.Magic;
			}
			if (item.CountsAsClass<SummonDamageClass>())
			{
				usedGemType = GemTechArmorGemType.Summoner;
			}
			if (item.CountsAsClass<ThrowingDamageClass>())
			{
				usedGemType = GemTechArmorGemType.Rogue;
			}
			if (PreviouslyUsedGem.HasValue && usedGemType != PreviouslyUsedGem)
			{
				MultiWeaponLifeRegenBonusCountdown = 150;
			}
			if (usedGemType.HasValue)
			{
				PreviouslyUsedGem = usedGemType;
			}
		}
	}

	public void PlayerOnHitEffects(int hitDamage)
	{
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer cgp = Owner.Calamity();
		if (!cgp.GemTechSet)
		{
			return;
		}
		bool gemWasLost = false;
		int gemDamage = 0;
		bool num = hitDamage >= 100;
		bool largeEnoughChaliceHit = cgp.chaliceOfTheBloodGod && hitDamage == ChaliceOfTheBloodGod.MinAllowedDamage && cgp.chaliceBleedoutBuffer >= 100.0;
		if (num | largeEnoughChaliceHit)
		{
			if (GemIsActive(GemTechArmorGemType.Rogue) && GemThatShouldBeLost == GemTechArmorGemType.Rogue)
			{
				RedGemRegenerationCountdown = 1800;
				gemDamage = (int)Owner.GetTotalDamage<ThrowingDamageClass>().ApplyTo(40000f);
				gemWasLost = true;
			}
			if (GemIsActive(GemTechArmorGemType.Melee) && GemThatShouldBeLost == GemTechArmorGemType.Melee)
			{
				YellowGemRegenerationCountdown = 1800;
				gemDamage = (int)Owner.GetTotalDamage<MeleeDamageClass>().ApplyTo(40000f);
				gemWasLost = true;
			}
			if (GemIsActive(GemTechArmorGemType.Ranged) && GemThatShouldBeLost == GemTechArmorGemType.Ranged)
			{
				GreenGemRegenerationCountdown = 1800;
				gemDamage = (int)Owner.GetTotalDamage<RangedDamageClass>().ApplyTo(40000f);
				gemWasLost = true;
			}
			if (GemIsActive(GemTechArmorGemType.Summoner) && GemThatShouldBeLost == GemTechArmorGemType.Summoner)
			{
				BlueGemRegenerationCountdown = 1800;
				gemDamage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(40000f);
				gemWasLost = true;
			}
			if (GemIsActive(GemTechArmorGemType.Magic) && GemThatShouldBeLost == GemTechArmorGemType.Magic)
			{
				PurpleGemRegenerationCountdown = 1800;
				gemDamage = (int)Owner.GetTotalDamage<MagicDamageClass>().ApplyTo(40000f);
				gemWasLost = true;
			}
			if (GemIsActive(GemTechArmorGemType.Base) && GemThatShouldBeLost == GemTechArmorGemType.Base)
			{
				PinkGemRegenerationCountdown = 1800;
				gemDamage = (int)Owner.GetTotalDamage<GenericDamageClass>().ApplyTo(40000f);
				gemWasLost = true;
			}
		}
		if (gemWasLost)
		{
			SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact);
			Vector2 gemPosition = CalculateGemPosition(GemThatShouldBeLost);
			gemDamage = CalamityUtils.DamageSoftCap(gemDamage, 100000);
			if (Main.myPlayer == OwnerIndex)
			{
				Projectile.NewProjectile(Owner.GetSource_ItemUse(Owner.HeldItem), gemPosition, Vector2.Zero, ModContent.ProjectileType<GemTechArmorGem>(), gemDamage, 0f, OwnerIndex, 0f, (float)GemThatShouldBeLost);
			}
		}
	}

	public void OnDeathEffects()
	{
		RedGemRegenerationCountdown = 0;
		YellowGemRegenerationCountdown = 0;
		GreenGemRegenerationCountdown = 0;
		BlueGemRegenerationCountdown = 0;
		PurpleGemRegenerationCountdown = 0;
		PinkGemRegenerationCountdown = 0;
	}

	public void ProvideGemBoosts()
	{
		if (!Owner.Calamity().GemTechSet)
		{
			return;
		}
		if (IsRedGemActive)
		{
			Owner.GetCritChance<ThrowingDamageClass>() += 16f;
			Owner.GetDamage<ThrowingDamageClass>() += 0.5f;
		}
		if (IsYellowGemActive)
		{
			Owner.GetCritChance<MeleeDamageClass>() += 12f;
			Owner.GetDamage<MeleeDamageClass>() += 0.45f;
		}
		if (IsGreenGemActive)
		{
			Owner.Calamity().ammoCost *= GemTechHeadgear.RangedAmmoReduction;
			Owner.GetCritChance<RangedDamageClass>() += 16f;
			Owner.GetDamage<RangedDamageClass>() += 0.5f;
		}
		if (IsBlueGemActive)
		{
			Owner.maxMinions += 4;
			Owner.GetDamage<SummonDamageClass>() += 0.72f;
		}
		if (IsPurpleGemActive)
		{
			Owner.statManaMax2 += 100;
			Owner.GetCritChance<MagicDamageClass>() += 16f;
			Owner.GetDamage<MagicDamageClass>() += 0.5f;
		}
		if (IsPinkGemActive)
		{
			Owner.statDefense += 75;
			Owner.lifeRegen += 2;
			Owner.moveSpeed += 0.4f;
			Owner.jumpSpeedBoost += 0.4f;
			Owner.endurance += 0.06f;
		}
		if (LifeRegenBonusCountdown > 0 && AllGemsActive)
		{
			if (MultiWeaponLifeRegenBonusCountdown > 0)
			{
				Owner.lifeRegen += 3;
			}
			else
			{
				Owner.lifeRegen += 2;
			}
		}
		if (!Owner.HeldItem.IsAir && !Owner.HeldItem.CountsAsClass<MagicDamageClass>())
		{
			Owner.manaRegen += 8;
		}
	}

	public float CalculateGemOffsetAngle(GemTechArmorGemType gemType, float time)
	{
		return (float)Math.PI * 2f * (float)gemType / 6f + time;
	}

	public Vector2 CalculateGemPosition(GemTechArmorGemType gemType)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		float gemTime = Main.GlobalTimeWrappedHourly * 3.41f;
		Vector2 baseDrawOffsetDirection = CalculateGemOffsetAngle(gemType, gemTime).ToRotationVector2() * new Vector2(1f, 0.2f);
		Vector2 gemPosition = Owner.Center + baseDrawOffsetDirection * (float)Owner.width * 1.25f;
		gemPosition.Y += Owner.gfxOffY;
		Mount mount = Owner.mount;
		if (mount != null && mount.Active)
		{
			gemPosition.Y += Owner.mount.YOffset;
		}
		return gemPosition;
	}

	public void CreateRegenerationEffect(GemTechArmorGemType gemType)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_DarkMageHealImpact, Owner.Center);
		for (int i = 0; i < 12; i++)
		{
			Dust dust = Dust.NewDustPerfect(CalculateGemPosition(gemType), 267);
			dust.velocity = ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2() * 5f;
			dust.color = GetColorFromGemType(gemType);
			dust.scale = 1.125f;
			dust.alpha = 175;
			dust.noGravity = true;
		}
	}

	public static Color GetColorFromGemType(GemTechArmorGemType gemType)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(gemType switch
		{
			GemTechArmorGemType.Rogue => new Color(224, 24, 0), 
			GemTechArmorGemType.Melee => new Color(237, 170, 43), 
			GemTechArmorGemType.Ranged => new Color(37, 188, 108), 
			GemTechArmorGemType.Summoner => new Color(37, 119, 206), 
			GemTechArmorGemType.Magic => new Color(200, 58, 209), 
			GemTechArmorGemType.Base => new Color(255, 115, 206), 
			_ => Color.Transparent, 
		});
	}
}
