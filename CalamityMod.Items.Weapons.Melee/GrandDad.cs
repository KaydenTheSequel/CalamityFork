using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class GrandDad : CustomUseProjItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle GrandDadEasterEggSound = new SoundStyle("CalamityMod/Sounds/Custom/GFB/GrandDad");

	public Vector2 oldVel;

	public int time;

	public float highestSpeed;

	public bool hitFloor;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 124;
		base.Item.height = 124;
		base.Item.damage = 2407;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 77;
		base.Item.useTime = 77;
		base.Item.useTurn = true;
		base.Item.knockBack = 77f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<GrandDadHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GrandDadGlow", (AssetRequestMode)2).Value);
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", Lang.SupportGlyphs(this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal")));
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MajesticGuard>().AddIngredient<TwistingNether>(3).AddTile(134)
			.Register();
	}

	public override void OnSpawn(IEntitySource source)
	{
		time = 0;
		hitFloor = false;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 place = base.Item.Center + Vector2.UnitY * 48f;
		if (time == 0 && base.Item.velocity.Y != 0f)
		{
			oldVel = base.Item.velocity;
			if (!hitFloor)
			{
				base.Item.velocity = new Vector2((float)(Math.Sign(base.Item.velocity.X) * 20), -10f);
			}
		}
		if (oldVel.Y != 0f && base.Item.velocity.Y == 0f)
		{
			time = 0;
			float power = Utils.GetLerpValue(35f, 75f, highestSpeed, clamped: true) * 2f;
			Main.LocalPlayer.SetScreenshake(3.5f * power);
			if (power > 0.25f)
			{
				float blastSize = 150f * power;
				float minMultiplier = 0.3f;
				int hitsToMinMult = 8;
				hitFloor = true;
				Projectile.NewProjectileDirect(base.Item.GetSource_FromThis(), place, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)(base.Item.damage * 2) * power), -35f * power, -1, blastSize, minMultiplier, hitsToMinMult).timeLeft = 5;
				int particleNumber = (int)Math.Max(15f * power, 2f);
				for (int i = -particleNumber; i <= particleNumber; i++)
				{
					GeneralParticleHandler.SpawnParticle(new AltSparkParticle(place, (Vector2.UnitX * (float)(5 + Math.Abs(i)) * (float)Math.Sign(i)).RotatedByRandom(0.25) * Main.rand.NextFloat(0.5f, 1f) * power, affectedByGravity: false, Main.rand.Next(17, 31), Main.rand.NextFloat(0.2f, 0.8f), Main.rand.NextBool() ? Color.Lerp(Color.Blue, Color.DodgerBlue, 0.3f) : Color.Gold));
					Dust dust = Dust.NewDustPerfect(place, ModContent.DustType<VoidDust>(), (Vector2.UnitX * (float)(5 + Math.Abs(i)) * (float)Math.Sign(i)).RotatedByRandom(0.25) * Main.rand.NextFloat(0.5f, 1f) * power, 0, default(Color), Main.rand.NextFloat(0.85f, 1.2f) * power);
					dust.noGravity = true;
					dust.color = (Main.rand.NextBool() ? Color.Blue : Color.DodgerBlue);
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit3");
				style.Volume = 0.6f * power;
				style.Pitch = Main.rand.NextFloat(0.4f, 0.6f);
				SoundEngine.PlaySound(in style, base.Item.Center);
				style = new SoundStyle("CalamityMod/Sounds/NPCHit/ThanatosHitOpen1");
				style.Volume = 0.45f * power;
				style.Pitch = -0.2f;
				SoundEngine.PlaySound(in style, base.Item.Center);
			}
			highestSpeed = 0f;
		}
		if (base.Item.velocity.Y != 0f)
		{
			if (base.Item.velocity.Y > 0f && base.Item.velocity.Y < 75f)
			{
				base.Item.velocity.X *= 0.98f;
				base.Item.velocity.Y += 0.35f;
				base.Item.velocity.Y *= 1.15f;
			}
			else
			{
				base.Item.velocity.Y += 0.55f;
			}
			time++;
			if (highestSpeed < base.Item.velocity.Y)
			{
				highestSpeed = base.Item.velocity.Y;
			}
		}
		if (base.Item.velocity.Y != 0f)
		{
			gravity = 0f;
		}
		maxFallSpeed = 75f;
		oldVel = base.Item.velocity;
	}

	public override void OnCreated(ItemCreationContext context)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in GrandDadEasterEggSound, Main.LocalPlayer.MountedCenter);
		}
	}

	public GrandDad()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		oldVel = Vector2.Zero;
		base._002Ector();
	}
}
