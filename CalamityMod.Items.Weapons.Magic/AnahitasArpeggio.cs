using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "SirensSong" })]
public class AnahitasArpeggio : ModItem, ILocalizedModType, IModType
{
	public float RotationOffset;

	public static readonly SoundStyle CapSound = new SoundStyle("CalamityMod/Sounds/Item/HarpLV6");

	public static readonly SoundStyle EndSound = new SoundStyle("CalamityMod/Sounds/Item/HarpEnd");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/HarpNoteHit");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 50;
		base.Item.damage = 60;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.attackSpeedOnlyAffectsWeaponAnimation = true;
		base.Item.useStyle = 12;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.knockBack = 6.5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AnahitasArpeggioNote>();
		base.Item.shootSpeed = 13f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.Calamity().arpeggioCooldown <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		int musicNoteCap = (Main.zenithWorld ? 7 : 6);
		int nonReleasedMusicNotes = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.type == base.Item.shoot && proj.owner == player.whoAmI && proj.ai[1] != 2f)
			{
				nonReleasedMusicNotes++;
			}
		}
		if (nonReleasedMusicNotes >= musicNoteCap)
		{
			if (!Main.zenithWorld)
			{
				SoundStyle style = CapSound with
				{
					Volume = 0.8f
				};
				SoundEngine.PlaySound(in style, player.Center);
			}
			return false;
		}
		if (nonReleasedMusicNotes <= 0)
		{
			RotationOffset = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		}
		Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, nonReleasedMusicNotes).ModProjectile<AnahitasArpeggioNote>()._randomReleaseRotationOffset = RotationOffset;
		return false;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		player.itemLocation.X -= 15f * (float)player.direction;
		player.itemLocation.Y += 15f * player.gravDir;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (Main.projectile.Count((Projectile proj) => proj.type == base.Item.shoot && Main.myPlayer == proj.owner && proj.active && proj.ai[1] != 2f) >= 6)
		{
			mult *= 0.25f;
		}
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine line = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[GFB]") && x.Mod == "Terraria");
		if (line != null)
		{
			line.Text = Lang.SupportGlyphs(this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
			if (Main.zenithWorld)
			{
				line.OverrideColor = Main.DiscoColor;
			}
		}
	}
}
