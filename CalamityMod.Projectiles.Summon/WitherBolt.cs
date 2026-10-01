using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WitherBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 25;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.width = (base.Projectile.height = (int)(72f * base.Projectile.scale));
		base.Projectile.scale = 0.8f;
		base.Projectile.timeLeft = 180;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		float oldSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		NPC potentialTarget = base.Projectile.Center.MinionHoming(800f, Main.player[base.Projectile.owner]);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 3f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * oldSpeed) / 4f;
		}
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * oldSpeed * 1.01f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 180);
		base.Projectile.timeLeft = 10;
		base.Projectile.Opacity = MathHelper.Lerp(base.Projectile.Opacity, 0f, 0.25f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D boltTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float completionRatio = (float)i / (float)base.Projectile.oldPos.Length;
			Color drawColor = Color.Lerp(lightColor, Color.Olive, 0.6f);
			drawColor = Color.Lerp(drawColor, Color.Black, completionRatio);
			drawColor = Color.Lerp(drawColor, Color.Transparent, completionRatio);
			Vector2 drawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Main.EntitySpriteDraw(boltTexture, drawPosition, null, base.Projectile.GetAlpha(drawColor), base.Projectile.oldRot[i], boltTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int i = 0; i < base.Projectile.oldPos.Length / 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.oldPos[i], base.Projectile.width / 2, base.Projectile.height / 2, 107);
				dust.velocity = (base.Projectile.oldRot[i] - (float)Math.PI / 2f).ToRotationVector2() * 4.5f + Main.rand.NextVector2Circular(2f, 2f);
				dust.color = Color.Olive;
				dust.noGravity = true;
			}
		}
	}
}
