using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class LightBlade : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 300;

	private const int NumAfterimages = 8;

	private const float LightBrightness = 0.7f;

	private const int DustID = 175;

	private const float SwordHomingStrength = 30f;

	private const float EnemyHomingStrength = 70f;

	private Color lightColor;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.scale = 1.5f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha = 0;
			FirstFrameEffects();
			base.Projectile.ai[0] = ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref lightColor)).ToVector3() * 0.7f);
		}
		if (base.Projectile.ai[1] > 0f)
		{
			Projectile paired = Main.projectile[(int)(base.Projectile.ai[1] - 1f)];
			if (paired == null || !paired.active || paired.type != ModContent.ProjectileType<LightBlade>())
			{
				base.Projectile.ai[1] = 0f;
			}
			else
			{
				Vector2 homingVec = base.Projectile.SafeDirectionTo(paired.Center) * base.Projectile.ai[0];
				base.Projectile.velocity = (base.Projectile.velocity * 29f + homingVec) / 30f;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 200f, base.Projectile.ai[0], 70f);
		float currentSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		Projectile projectile = base.Projectile;
		projectile.velocity *= base.Projectile.ai[0] / currentSpeed;
	}

	private void FirstFrameEffects()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frame = Main.rand.Next(14);
		lightColor = TheDanceofLight.GetRandomLightColor();
		Vector2 baseOffsetVec = default(Vector2);
		((Vector2)(ref baseOffsetVec))._002Ector(1f, 4f);
		int numDust = 16;
		for (int i = 0; i < numDust; i++)
		{
			Vector2 dustOffset = Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f) / (float)numDust) * baseOffsetVec;
			dustOffset = dustOffset.RotatedBy(base.Projectile.velocity.ToRotation());
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, 175);
			dust.position = base.Projectile.Center + dustOffset;
			dust.velocity = dustOffset.SafeNormalize(Vector2.Zero);
			dust.velocity *= 1.4f;
			dust.scale = 1.5f;
			dust.noGravity = true;
		}
		float startingBrightness = 2.1f;
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref lightColor)).ToVector3() * startingBrightness);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer calPlayer = player.Calamity();
		calPlayer.danceOfLightCharge++;
		if (calPlayer.danceOfLightCharge >= 300)
		{
			calPlayer.danceOfLightCharge = 0;
			if (base.Projectile.owner == Main.myPlayer)
			{
				int flashDamage = (int)player.GetTotalDamage<MagicDamageClass>().ApplyTo(16000f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BlindingLight>(), flashDamage, 0f, base.Projectile.owner);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit3, base.Projectile.Center);
		int numDust = Main.rand.Next(4, 10);
		for (int i = 0; i < numDust; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, 175, 0f, 0f, 100);
			dust.velocity *= 1.6f;
			dust.velocity += base.Projectile.velocity * Main.rand.NextFloat(-0.5f, 0.5f);
			dust.velocity.Y += -1f;
			dust.noGravity = true;
			dust.scale = 2f;
			dust.fadeIn = 0.5f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return lightColor;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		Point projTile = base.Projectile.Center.ToTileCoordinates();
		Color localLight = Lighting.GetColor(projTile.X, projTile.Y);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Rectangle rect = default(Rectangle);
		((Rectangle)(ref rect))._002Ector(38 * base.Projectile.frame, 0, 38, 38);
		Vector2 halfSpriteSize = rect.Size() / 2f;
		Color mainSwordColor = base.Projectile.GetAlpha(localLight);
		((Color)(ref mainSwordColor)).A = 160;
		SpriteEffects sfx = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Main.EntitySpriteDraw(tex, drawPos, rect, mainSwordColor, base.Projectile.rotation, halfSpriteSize, base.Projectile.scale, sfx);
		for (int i = 0; i < 8; i++)
		{
			Color afterimageLight = base.Projectile.GetAlpha(localLight);
			((Color)(ref afterimageLight)).A = 40;
			float posLerp = 1f;
			Vector2 val = (1f - posLerp) * base.Projectile.position + posLerp * base.Projectile.oldPos[i];
			float rotation = base.Projectile.oldRot[i];
			SpriteEffects afterimageSfx = (SpriteEffects)(base.Projectile.oldSpriteDirection[i] == -1);
			float afterimageScale = MathHelper.Lerp(1.2f * base.Projectile.scale, 0.4f * base.Projectile.scale, (float)i / 7f);
			Vector2 imageDrawPos = val + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
			Main.EntitySpriteDraw(tex, imageDrawPos, rect, afterimageLight, rotation, halfSpriteSize, afterimageScale, afterimageSfx);
		}
		return false;
	}

	public LightBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.White;
		base._002Ector();
	}
}
