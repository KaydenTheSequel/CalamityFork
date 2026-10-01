using System;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "DraedonsExoblade" })]
public class Exoblade : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle SwingSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeSwing")
	{
		MaxInstances = 3,
		PitchVariance = 0.6f,
		Volume = 0.8f
	};

	public static readonly SoundStyle BigSwingSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBigSwing")
	{
		MaxInstances = 3,
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle BigHitSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBigHit")
	{
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle BeamHitSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash")
	{
		Volume = 0.4f,
		PitchVariance = 0.2f
	};

	public static readonly SoundStyle DashSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeDash")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle DashHitSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeDashImpact")
	{
		Volume = 0.85f
	};

	public static int BeamNoHomeTime = 24;

	public static float NotTrueMeleeDamagePenalty = 0.35f;

	public static float ExplosionDamageFactor = 1.8f;

	public static float LungeDamageFactor = 1.75f;

	public static int LungeCooldown = 180;

	public static float LungeMaxCorrection = (float)Math.PI / 80f;

	public static float LungeSpeed = 60f;

	public static float ReboundSpeed = 6f;

	public static float PercentageOfAnimationSpentLunging = 0.6f;

	public static int OpportunityForBigSlash = 111;

	public static float BigSlashUpscaleFactor = 1.5f;

	public static int DashTime = 49;

	public static int BaseUseTime = 49;

	public static int BeamsPerSwing = 4;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 138;
		base.Item.height = 184;
		base.Item.damage = 915;
		base.Item.useStyle = 1;
		base.Item.useTime = BaseUseTime;
		base.Item.useAnimation = BaseUseTime;
		base.Item.useTurn = true;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.knockBack = 9f;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<ExobladeProj>();
		base.Item.shootSpeed = 9f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanShoot(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<ExobladeProj>());
		}
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<ExobladeProj>() && (n.ai[0] != 1f || n.ai[1] != 1f));
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().rightClickListener = true;
		player.Calamity().mouseWorldListener = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? CanHitNPC(Player player, NPC target)
	{
		return false;
	}

	public override bool CanHitPvp(Player player, Player target)
	{
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		float state = 0f;
		bool empoweredSlash = false;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == player.whoAmI && p.type == base.Item.shoot && p.ai[0] == 1f && p.ai[1] == 1f && p.timeLeft > LungeCooldown)
			{
				empoweredSlash = true;
				break;
			}
		}
		if (empoweredSlash)
		{
			state = 2f;
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile p2 = enumerator2.Current;
				if (p2.owner == player.whoAmI && p2.type == base.Item.shoot && p2.ai[0] == 1f && p2.ai[1] == 1f)
				{
					p2.timeLeft = LungeCooldown;
					p2.ForceNetUpdate();
				}
			}
		}
		if (player.altFunctionUse == 2)
		{
			state = 1f;
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, state);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ExobladeGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Terratomere>().AddIngredient<Lightspeed>().AddIngredient<EntropicClaymore>()
			.AddIngredient<FlarefrostBlade>()
			.AddIngredient<MiracleMatter>()
			.AddTile(ModContent.TileType<DraedonsForge>())
			.Register();
	}
}
