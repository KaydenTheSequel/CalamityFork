using System;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "WulfrumStaff" })]
public class WulfrumProsthesis : ModItem, IHideFrontArm, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumProsthesisShoot")
	{
		PitchVariance = 0.1f,
		Volume = 0.55f
	};

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumProsthesisHit")
	{
		PitchVariance = 0.1f,
		Volume = 0.75f,
		MaxInstances = 3
	};

	public static readonly SoundStyle SuckSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumProsthesisSucc")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle SuckStopSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumProsthesisSuccStop")
	{
		Volume = 0.5f
	};

	internal static Asset<Texture2D> RealSprite;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override string Texture => "CalamityMod/Items/Weapons/Magic/WulfrumProsthesis_Arm";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 42;
		base.Item.damage = 18;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 5;
		base.Item.useTime = 24;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.UseSound = ShootSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<WulfrumBolt>();
		base.Item.shootSpeed = 18f;
		base.Item.holdStyle = 16;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
		player.Calamity().rightClickListener = true;
	}

	public override bool CanUseItem(Player player)
	{
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<WulfrumManaDrain>());
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			type = ModContent.ProjectileType<WulfrumManaDrain>();
		}
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.altFunctionUse == 2)
		{
			mult = 0f;
		}
	}

	public override void UseAnimation(Player player)
	{
		base.Item.UseSound = ShootSound;
		if (player.altFunctionUse == 2)
		{
			base.Item.UseSound = null;
		}
	}

	public void SetItemInHand(Player player, Rectangle heldItemFrame)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().mouseWorld.X > player.Center.X)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		float animProgress = 1f - (float)player.itemTime / (float)player.itemTimeMax;
		Vector2 itemPosition = player.MountedCenter + new Vector2(-2f * (float)player.direction, -1f * player.gravDir);
		float itemRotation = (player.Calamity().mouseWorld - itemPosition).ToRotation();
		if (animProgress < 0.7f)
		{
			itemPosition -= itemRotation.ToRotationVector2() * (1f - (float)Math.Pow(1f - (0.7f - animProgress) / 0.7f, 4.0)) * 4f;
		}
		if (animProgress < 0.4f)
		{
			itemRotation += -0.45f * (float)Math.Pow((0.4f - animProgress) / 0.4f, 2.0) * (float)player.direction * player.gravDir;
		}
		if (player.itemTime == 1 && Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<WulfrumManaDrain>()))
		{
			itemPosition += Main.rand.NextVector2Circular(2f, 2f);
		}
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(28f, 14f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-8f, 0f);
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin, noSandstorm: true);
	}

	public override void HoldStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (RealSprite == null)
		{
			RealSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/WulfrumProsthesis", (AssetRequestMode)2);
		}
		Texture2D properSprite = RealSprite.Value;
		spriteBatch.DrawNewInventorySprite(properSprite, new Vector2(28f, 14f), position, drawColor, origin, scale, (Vector2?)new Vector2(0f, -6f));
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (RealSprite == null)
		{
			RealSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/WulfrumProsthesis", (AssetRequestMode)2);
		}
		Texture2D properSprite = RealSprite.Value;
		spriteBatch.Draw(properSprite, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, properSprite.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(10).AddTile(16).Register();
	}
}
