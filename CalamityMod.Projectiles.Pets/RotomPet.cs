using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class RotomPet : ModProjectile, ILocalizedModType, IModType
{
	private enum Form
	{
		Normal,
		Dex,
		Wash,
		Heat,
		Frost,
		Mow,
		Fan
	}

	private bool initialized;

	private Form RotomType;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-10f, 0f).WithSpriteDirection(1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.rotomPet = false;
		}
		if (modPlayer.rotomPet)
		{
			base.Projectile.timeLeft = 2;
		}
		if (!initialized)
		{
			DustEffects();
			initialized = true;
		}
		UpdateForm(player);
		UpdateFrames();
		base.Projectile.FloatingPetAI(faceRight: true, 0.05f);
	}

	private void UpdateForm(Player player)
	{
		if (CalamityPlayer.areThereAnyDamnBosses)
		{
			RotomType = Form.Dex;
		}
		else if (player.ZoneBeach || player.InSunkenSea() || player.InSulphur() || player.InAbyss())
		{
			RotomType = Form.Wash;
		}
		else if (player.ZoneTowerSolar || player.ZoneDesert || player.ZoneUndergroundDesert || player.ZoneUnderworldHeight || player.InCalamity())
		{
			RotomType = Form.Heat;
		}
		else if (player.ZoneSnow || Main.snowMoon)
		{
			RotomType = Form.Frost;
		}
		else if (player.ZoneJungle)
		{
			RotomType = Form.Mow;
		}
		else if (player.ZoneSkyHeight || player.ZoneMeteor || player.InAstral())
		{
			RotomType = Form.Fan;
		}
		else
		{
			RotomType = Form.Normal;
		}
	}

	private void DustEffects()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		int dustAmt = 25;
		for (int i = 0; i < dustAmt; i++)
		{
			int electric = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 132);
			Dust obj = Main.dust[electric];
			obj.velocity *= 2f;
			Main.dust[electric].scale *= 1.15f;
		}
	}

	private void UpdateFrames()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Drawing(lightColor, TextureAssets.Projectile[base.Type].Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomDex", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomWash", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomHeat", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomFrost", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomMow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomFan", (AssetRequestMode)2).Value);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Drawing(Color.White, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomPetGlow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomDexGlow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomWashGlow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomHeatGlow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomFrostGlow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomMowGlow", (AssetRequestMode)2).Value, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/RotomFanGlow", (AssetRequestMode)2).Value);
	}

	private void Drawing(Color color, Texture2D normal, Texture2D dex, Texture2D wash, Texture2D heat, Texture2D frost, Texture2D mow, Texture2D fan)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = normal;
		switch (RotomType)
		{
		case Form.Dex:
			texture = dex;
			break;
		case Form.Wash:
			texture = wash;
			break;
		case Form.Heat:
			texture = heat;
			break;
		case Form.Frost:
			texture = frost;
			break;
		case Form.Mow:
			texture = mow;
			break;
		case Form.Fan:
			texture = fan;
			break;
		}
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), color, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
