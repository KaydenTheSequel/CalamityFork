using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AstralProbeRound : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/AstralLaser";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = 600;
		base.Projectile.width = 62;
		base.Projectile.height = 20;
		base.Projectile.alpha = 125;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 3 % Main.projFrames[base.Type];
		Lighting.AddLight(base.Projectile.Center, Vector3.One * 0.1f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			Color white = Color.White;
			((Color)(ref white)).A = 25;
			Color afterimageDrawColor = white * base.Projectile.Opacity * (1f - (float)i / (float)base.Projectile.oldPos.Length);
			Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, base.Projectile.rotation, origin, base.Projectile.scale, direction);
		}
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			int randomDust = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralOrange>(), ModContent.DustType<AstralBlue>());
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, randomDust, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
