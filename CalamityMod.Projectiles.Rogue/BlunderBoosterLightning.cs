using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BlunderBoosterLightning : ModProjectile, ILocalizedModType, IModType
{
	public static int frameWidth = 12;

	public static int frameHeight = 26;

	public int dir;

	public float intensity;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 290;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 15;
	}

	public override void AI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.ai[1]++;
		if (dir == 0)
		{
			base.Projectile.timeLeft -= Main.rand.Next(5, 21);
			dir = ((!Main.rand.NextBool()) ? 1 : (-1));
			intensity = Main.rand.NextFloat(0.2f, 1.2f);
		}
		if (base.Projectile.timeLeft < 190)
		{
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, base.Projectile.Center.ClosestNPCAt(1500f), ignoreTiles: true, 0.65f * intensity, 8f, 0.98f, 0.99f, accelerate: true);
		}
		else
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.04f * (float)dir * intensity) * Main.rand.NextFloat(0.985f, 1f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<VermillionFlux>(), 90);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<VermillionFlux>(), 90);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = ((base.Projectile.ai[0] != 0f) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/BlunderBoosterLightning2", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/BlunderBoosterLightning", (AssetRequestMode)2).Value);
		Color drawColour = Color.White;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(frameWidth / 2), (float)(frameHeight / 2));
		Main.EntitySpriteDraw(sprite, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, frameHeight * base.Projectile.frame, frameWidth, frameHeight), drawColour, base.Projectile.rotation, origin, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item93 with
		{
			Volume = SoundID.Item93.Volume * 0.25f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		for (int i = 0; i < 5; i++)
		{
			int dustType = 60;
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 0.5f);
			Main.dust[dust].noGravity = true;
		}
	}
}
