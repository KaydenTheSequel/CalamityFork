using System;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Hydra : ModItem, ILocalizedModType, IModType
{
	public const int BulletsPerShot = 3;

	public const float ShotSpread = 10f;

	public const int TimeToSpawnHead = 3;

	public const int MaximumHeadCount = 3;

	public int HeadID = ModContent.ProjectileType<HydraHead>();

	public int HeadSpawnTimer;

	public static readonly SoundStyle SpawnSound = SoundID.Item60;

	public static readonly SoundStyle LaunchSound = SoundID.Item1;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.damage = 44;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 66);
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.shoot = ModContent.ProjectileType<HydrasBlood>();
		base.Item.shootSpeed = 10f;
		base.Item.knockBack = 10f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = null;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.altFunctionUse != 2;
	}

	public override void UpdateInventory(Player player)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (player.HeldItem == base.Item && player.ownedProjectileCounts[HeadID] < 3)
		{
			HeadSpawnTimer++;
			if (HeadSpawnTimer >= 180)
			{
				HeadSpawnTimer = 0;
				SoundEngine.PlaySound(in SpawnSound, player.Center);
				Projectile.NewProjectileDirect(base.Item.GetSource_FromThis(), player.Top + Vector2.UnitY * 8f, Vector2.Zero, HeadID, 0, 0f, player.whoAmI).OriginalCritChance = base.Item.crit;
			}
		}
		else
		{
			if (player.HeldItem.type == base.Item.type && !player.dead && player.active)
			{
				return;
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == HeadID && p.owner == player.whoAmI)
				{
					p.Kill();
				}
			}
			HeadSpawnTimer = 0;
		}
	}

	public override bool AltFunctionUse(Player player)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[HeadID] <= 0)
		{
			return false;
		}
		SoundEngine.PlaySound(in LaunchSound, player.Center);
		for (int i = 0; i < Main.maxProjectiles; i++)
		{
			if (Main.projectile[i].type == HeadID && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].active)
			{
				Main.projectile[i].ai[1] = -1f;
			}
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = velocity.SafeNormalize(Vector2.Zero);
		for (int i = 0; i < 3; i++)
		{
			int CurrentHeadCount = player.ownedProjectileCounts[HeadID];
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/Hydra");
			soundStyle.Volume = 0.3f + (float)(Math.Pow(CurrentHeadCount, 1.2) * 0.15000000596046448);
			SoundStyle FireSound = soundStyle;
			SoundEngine.PlaySound(in FireSound, player.Center);
			Vector2 spreadDirection = shootDirection.RotatedByRandom(MathHelper.ToRadians(5f));
			float spreadVelocity = base.Item.shootSpeed * Main.rand.NextFloat(1f, 1.4f);
			Vector2 newPos = player.MountedCenter + shootDirection * 50f;
			Projectile.NewProjectile(source, newPos, spreadVelocity * spreadDirection, base.Item.shoot, damage, knockback, player.whoAmI);
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == HeadID && p.owner == player.whoAmI)
			{
				p.ai[1] = 1f;
			}
		}
		return false;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 7f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector((float)base.Item.width, (float)base.Item.height);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-17f, 3f);
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
		base.UseStyle(player, heldItemFrame);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float animProgress = 1f - (float)player.itemTime / (float)player.itemTimeMax;
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		if (animProgress < 0.4f)
		{
			rotation += -0.45f * (float)Math.Pow((0.4f - animProgress) / 0.4f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4703).AddIngredient(1346, 25).AddIngredient(1339, 25)
			.AddTile(134)
			.Register();
	}
}
