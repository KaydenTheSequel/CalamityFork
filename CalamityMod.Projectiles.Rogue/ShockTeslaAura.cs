using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ShockTeslaAura : ModProjectile, ILocalizedModType, IModType
{
	private const float radius = 98f;

	private const int lifetime = 240;

	private const int framesX = 3;

	private const int framesY = 6;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Typeless/TeslaAura";

	public override void SetDefaults()
	{
		base.Projectile.width = 218;
		base.Projectile.height = 218;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 240;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.localAI[0]++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.localAI[0] >= 6f)
		{
			base.Projectile.localAI[0] = 0f;
			base.Projectile.localAI[1]++;
		}
		if (base.Projectile.localAI[1] >= 3f)
		{
			base.Projectile.localAI[1] = 0f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = TextureAssets.Projectile[base.Type].Value;
		Color drawColour = Color.White;
		Rectangle sourceRect = default(Rectangle);
		((Rectangle)(ref sourceRect))._002Ector(base.Projectile.width * (int)base.Projectile.localAI[1], base.Projectile.height * (int)base.Projectile.localAI[0], base.Projectile.width, base.Projectile.height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(base.Projectile.width / 2), (float)(base.Projectile.height / 2));
		float opacity = 1f;
		int sparkCount = 0;
		int fadeTime = 20;
		if (base.Projectile.timeLeft < fadeTime)
		{
			opacity = (float)base.Projectile.timeLeft * (1f / (float)fadeTime);
			sparkCount = fadeTime - base.Projectile.timeLeft;
		}
		Vector2 dustPos = default(Vector2);
		for (int i = 0; i < sparkCount * 2; i++)
		{
			int dustType = 132;
			if (Main.rand.NextBool())
			{
				dustType = 264;
			}
			float rangeDiff = 2f;
			((Vector2)(ref dustPos))._002Ector(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
			((Vector2)(ref dustPos)).Normalize();
			dustPos *= 98f + Main.rand.NextFloat(0f - rangeDiff, rangeDiff);
			int dust = Dust.NewDust(base.Projectile.Center + dustPos, 1, 1, dustType, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[dust].noGravity = true;
		}
		Main.EntitySpriteDraw(sprite, base.Projectile.Center - Main.screenPosition, sourceRect, drawColour * opacity, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 60);
		target.AddBuff(ModContent.BuffType<GalvanicCorrosion>(), 60);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (target.Center.X > base.Projectile.Center.X).ToDirectionInt();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(144, 60);
		target.AddBuff(ModContent.BuffType<GalvanicCorrosion>(), 60);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 98f, targetHitbox);
	}
}
