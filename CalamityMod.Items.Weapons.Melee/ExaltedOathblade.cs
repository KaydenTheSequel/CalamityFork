using System;
using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "TrueForbiddenOathblade" })]
public class ExaltedOathblade : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public int throwCount;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 88;
		base.Item.damage = 90;
		base.Item.useAnimation = (base.Item.useTime = 45);
		base.Item.useStyle = 5;
		base.Item.shoot = ModContent.ProjectileType<ExaltedOathbladeThrownBlade>();
		base.Item.useTurn = true;
		base.Item.knockBack = 5.5f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 31f;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<ExaltedOathbladeHoldout>()] <= 0)
		{
			return !player.Calamity().mouseRight;
		}
		return false;
	}

	public override void HoldItem(Player player)
	{
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (player.Calamity().mouseRight && !player.mouseInterface && player.Calamity().killModeCooldown == 0 && !Main.mapFullscreen && !Main.blockMouse)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordKillMode");
			style.Volume = 0.95f;
			SoundEngine.PlaySound(in style, player.Center);
			for (int i = 0; i < 10; i++)
			{
				Vector2 vel = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * 6.5f;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center + vel * 14f, -vel * 0.1f, "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 22, 0.6f, ((i % 2 == 0) ? Color.MediumOrchid : Color.BlueViolet) * 0.7f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.23f));
				Dust dust = Dust.NewDustPerfect(player.Center, ModContent.DustType<LightDust>());
				dust.velocity = vel;
				dust.scale = 1.7f;
				dust.noGravity = true;
				dust.color = ((i % 2 != 0) ? Color.MediumOrchid : Color.BlueViolet);
				dust.noLightEmittence = true;
			}
			player.Calamity().demonSwordKillMode = true;
			int cooldownTime = KillMode.cooldownMax + KillMode.buffMax;
			player.Calamity().killModeCooldown = cooldownTime;
			player.AddCooldown(KillMode.ID, cooldownTime);
		}
		if (player.Calamity().demonSwordKillMode && player.ownedProjectileCounts[ModContent.ProjectileType<ExaltedOathbladeHoldout>()] <= 0 && player.Calamity().killModeCooldown == KillMode.cooldownMax + KillMode.buffMax)
		{
			Projectile.NewProjectile(player.GetSource_FromThis(), player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<ExaltedOathbladeHoldout>(), base.Item.damage * 16, base.Item.knockBack, player.whoAmI, 0f, throwCount);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		throwCount++;
		int useSpeed = (int)MathHelper.Clamp((float)base.Item.useTime / 2.8f, 1f, 100f);
		Projectile projectile = Projectile.NewProjectileDirect(source, player.MountedCenter, velocity, type, damage, knockback, player.whoAmI, 0f, throwCount);
		projectile.localAI[2] = useSpeed;
		projectile.timeLeft += useSpeed;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ForbiddenOathblade>().AddIngredient(1570).AddIngredient<AshesofCalamity>(8)
			.AddIngredient<ScoriaBar>(8)
			.AddTile(134)
			.Register();
	}
}
