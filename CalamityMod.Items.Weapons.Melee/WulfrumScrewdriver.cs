using System;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "WulfrumBlade" })]
public class WulfrumScrewdriver : ModItem, ILocalizedModType, IModType
{
	public static int DefaultTime;

	public static readonly SoundStyle ThrustSound;

	public static readonly SoundStyle ThudSound;

	public static readonly SoundStyle ScrewGetSound;

	public static readonly SoundStyle ScrewHitSound;

	public static readonly SoundStyle FunnyUltrablingSound;

	public static bool ScrewQeuedForStorage;

	public bool ScrewStored;

	public static Vector3 ScrewStart;

	public static Vector3 ScrewPosition;

	public static Vector2 PrevOffset;

	public static float ScrewTimer;

	public static float ScrewTime;

	public static Asset<Texture2D> ScrewTex;

	public static Asset<Texture2D> ScrewOutlineTex;

	public static float ScrewBaseDamageMult;

	public static float ScrewBazingaModeDamageMult;

	public static float ScrewBazingaAimAssistAngle;

	public static float ScrewBazingaAimAssistReach;

	public CalamityUtils.CurveSegment InitialAway = new CalamityUtils.CurveSegment(CalamityUtils.SineOutEasing, 0f, 0f, -0.2f, 3);

	public CalamityUtils.CurveSegment AccelerateTowards = new CalamityUtils.CurveSegment(CalamityUtils.PolyInEasing, 0.3f, -0.2f, 1.2f, 3);

	public CalamityUtils.CurveSegment Bump1Segment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.5f, 1f, 0.24f);

	public CalamityUtils.CurveSegment Bump2Segment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.8f, 1f, -0.1f);

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public bool ScrewAvailable
	{
		get
		{
			if (ScrewStored)
			{
				return ScrewTimer == 0f;
			}
			return false;
		}
	}

	internal float ProgressionOfScrew => CalamityUtils.PiecewiseAnimation(ScrewTimer / ScrewTime, InitialAway, AccelerateTowards, Bump1Segment, Bump2Segment);

	public override ModItem Clone(Item item)
	{
		return base.Clone(item);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 2f;
		}
		return base.UseSpeedMultiplier(player);
	}

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 50;
		base.Item.damage = 12;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = DefaultTime + WulfrumScrewdriverProj.MaxTime;
		base.Item.useStyle = 5;
		base.Item.useTime = DefaultTime + WulfrumScrewdriverProj.MaxTime;
		base.Item.useTurn = true;
		base.Item.knockBack = 3.75f;
		base.Item.UseSound = ThrustSound;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<WulfrumScrewdriverProj>();
		base.Item.shootSpeed = 1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(10).AddTile(16).Register();
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		ScrewStored = false;
	}

	public override void UpdateInventory(Player player)
	{
		if (player.HeldItem != base.Item)
		{
			ScrewStored = false;
		}
	}

	public override void HoldItem(Player player)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		if (Main.myPlayer == player.whoAmI)
		{
			if (ScrewQeuedForStorage)
			{
				ScrewStored = true;
				ScrewQeuedForStorage = false;
			}
			if (ScrewTimer > 0f)
			{
				ScrewTimer--;
			}
			if (ScrewTimer == 1f)
			{
				Vector2 dustPos = new Vector2(ScrewPosition.X, ScrewPosition.Y) + Main.screenPosition;
				int numDust = Main.rand.Next(5, 15);
				for (int i = 0; i < numDust; i++)
				{
					int type = (Main.rand.NextBool() ? 246 : 247);
					Vector2? velocity = Main.rand.NextVector2Circular(1f, 1f);
					float scale = Main.rand.NextFloat(0.9f, 1.4f);
					Dust.NewDustPerfect(dustPos, type, velocity, 0, default(Color), scale);
				}
				SoundEngine.PlaySound(in ScrewGetSound);
			}
		}
		base.HoldItem(player);
	}

	public override bool AltFunctionUse(Player player)
	{
		return ScrewAvailable;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return true;
		}
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<WulfrumScrew>());
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			damage = (int)((float)damage * ScrewBaseDamageMult);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Vector2 chuckSpeed = new Vector2((float)Math.Sign(velocity.X) * 0.4f, -1.24f) + player.velocity / 4f;
			chuckSpeed.Y = Math.Clamp(chuckSpeed.Y, -1f, 3f);
			Projectile.NewProjectile(source, position, chuckSpeed, ModContent.ProjectileType<WulfrumScrew>(), damage, knockback, player.whoAmI);
			ScrewStored = false;
			return false;
		}
		return base.Shoot(player, source, position, velocity, type, damage, knockback);
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		if (!ScrewStored)
		{
			return;
		}
		Player myPlayer = Main.LocalPlayer;
		if (myPlayer.HeldItem == base.Item && myPlayer.active && !myPlayer.dead)
		{
			spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			if (ScrewTex == null)
			{
				ScrewTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/WulfrumScrew", (AssetRequestMode)2);
			}
			if (ScrewOutlineTex == null)
			{
				ScrewOutlineTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/WulfrumScrewOutline", (AssetRequestMode)2);
			}
			Texture2D screwTex = ScrewTex.Value;
			Texture2D screwOutlineTex = ScrewOutlineTex.Value;
			Vector2 realIdealSpot = myPlayer.MountedCenter + myPlayer.gfxOffY * Vector2.UnitY - Main.screenPosition - Vector2.UnitY * 50f - Vector2.Lerp(myPlayer.velocity, PrevOffset, 0.5f);
			realIdealSpot.Y += (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 5f;
			realIdealSpot.X += (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1f) * 7.8f;
			ScrewPosition = new Vector3(realIdealSpot, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f) * ((float)Math.PI / 4f) * 0.34f);
			position = Vector2.Lerp(new Vector2(ScrewPosition.X, ScrewPosition.Y), new Vector2(ScrewStart.X, ScrewStart.Y), ProgressionOfScrew);
			float rotation = ScrewPosition.Z.AngleLerp(ScrewStart.Z, ProgressionOfScrew);
			float outlineOpacity = (float)Math.Pow(1f - ScrewTimer / ScrewTime, 2.0);
			scale = 1.05f + 0.05f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f);
			Main.spriteBatch.Draw(screwOutlineTex, position, (Rectangle?)null, Color.Lerp(Color.GreenYellow, Color.White, (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f + 0.5f) * outlineOpacity, rotation, screwOutlineTex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(screwTex, position, (Rectangle?)null, Color.White, rotation, screwTex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			PrevOffset = myPlayer.velocity;
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}

	static WulfrumScrewdriver()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		DefaultTime = 10;
		ThrustSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumScrewdriverThrust")
		{
			PitchVariance = 0.4f
		};
		ThudSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumScrewdriverThud")
		{
			PitchVariance = 0.2f,
			Volume = 0.7f
		};
		ScrewGetSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumScrewdriverScrewGet")
		{
			PitchVariance = 0.1f
		};
		ScrewHitSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumScrewdriverScrewHit")
		{
			Volume = 0.7f
		};
		FunnyUltrablingSound = new SoundStyle("CalamityMod/Sounds/Custom/UltrablingHit");
		ScrewQeuedForStorage = false;
		ScrewStart = new Vector3(0f);
		ScrewTime = 40f;
		ScrewBaseDamageMult = 1.5f;
		ScrewBazingaModeDamageMult = 6.5f;
		ScrewBazingaAimAssistAngle = 0.52f;
		ScrewBazingaAimAssistReach = 600f;
	}
}
