using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class PristineFury : LegendaryItem, ILocalizedModType, IModType
{
	public int frameCounter;

	public int frame;

	public bool Trail = true;

	public int shotCount;

	public static int boomTime = 6;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override Color? TooltipExtensionColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 140, 0);
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 46;
		base.Item.damage = 77;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 3;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item34;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PristineFire>();
		base.Item.shootSpeed = 11f;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.consumeAmmoOnFirstShotOnly = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-25f, -10f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override float UseTimeMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 0.5f;
	}

	public override void HoldItem(Player player)
	{
		int max = 1800;
		if (player.Calamity().cooldowns.TryGetValue(FuryFuel.ID, out var cooldown))
		{
			cooldown.timeLeft = max - player.Calamity().furyFuel;
		}
		else
		{
			player.AddCooldown(FuryFuel.ID, max);
		}
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.altFunctionUse != 2;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			player.Calamity().furyRefuelTimer = -50f;
			if (player.Calamity().furyFuel > 0)
			{
				Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(5f));
				Projectile.NewProjectile(source, position, newVel, ModContent.ProjectileType<PristineSecondary>(), (int)((float)damage * 0.25f), knockback, player.whoAmI);
				Dust dust = Dust.NewDustPerfect(position + velocity * 3f + new Vector2(0f, -3f), ModContent.DustType<LightDust>(), velocity.RotatedBy(0.25f * (float)player.direction).RotatedByRandom(0.3499999940395355) * Main.rand.NextFloat(0.5f, 2.5f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.8f));
				dust.noGravity = true;
				dust.color = Color.Orchid;
				GeneralParticleHandler.SpawnParticle(new CritSpark(position + velocity * 3f + new Vector2(0f, -3f), velocity.RotatedBy(0.25f * (float)player.direction).RotatedByRandom(0.25) * Main.rand.NextFloat(0.2f, 1.8f), Color.White, Color.Orchid, 0.9f, 18, 2f, 2.2f));
				player.Calamity().furyFuel -= 15;
			}
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity * 0.8f, type, damage, knockback, player.whoAmI, Trail ? 1 : 0, 0f, shotCount);
			Trail = !Trail;
			for (int i = 0; i <= 2; i++)
			{
				Dust.NewDustPerfect(position + velocity * 3f + new Vector2(0f, -3f), 158, velocity.RotatedBy(0.25f * (float)player.direction).RotatedByRandom(0.3499999940395355) * Main.rand.NextFloat(0.5f, 2.5f), 0, default(Color), Main.rand.NextFloat(1.6f, 2f)).noGravity = true;
			}
			GeneralParticleHandler.SpawnParticle(new CritSpark(position + velocity * 3f + new Vector2(0f, -3f), velocity.RotatedBy(0.25f * (float)player.direction).RotatedByRandom(0.25) * Main.rand.NextFloat(0.2f, 1.8f), Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed, Color.OrangeRed, 0.9f, 18, 2f, 1.9f));
			shotCount++;
		}
		return false;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Animated", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, position, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 4), Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Animated", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 4), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 4, frameCounterUp: false), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
	}
}
