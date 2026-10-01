using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class WhitewaterAura : ModProjectile, ILocalizedModType, IModType
{
	public float fade;

	public float areaScale = 1f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 5);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += Main.rand.NextFloat(0.09f, 0.23f);
		areaScale = Math.Abs((float)Math.Sin(time * 0.175f / (float)Math.PI)) * 0.05f + 1f;
		for (int playerIndex = 0; playerIndex < 255; playerIndex++)
		{
			Player player = Main.player[playerIndex];
			if (Vector2.Distance(player.Center, base.Projectile.Center) < 200f * areaScale && player.Calamity().whitewaterHeal == 0)
			{
				player.Calamity().whitewaterHeal = ((player.whoAmI == base.Projectile.owner) ? 300 : 600);
			}
		}
		if (base.Projectile.timeLeft < 30)
		{
			fade = MathHelper.Lerp(fade, 0f, 0.12f);
			areaScale = MathHelper.Lerp(areaScale, 0f, 0.12f);
		}
		if (time < 30f)
		{
			fade = MathHelper.Lerp(fade, 1f, 0.12f);
		}
		else if (base.Projectile.timeLeft > 30)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2CircularEdge(200f * areaScale, 200f * areaScale), 66);
				dust.scale = Main.rand.NextFloat(0.3f, 0.7f);
				dust.velocity = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.5f, 1f);
				dust.color = Color.LightBlue;
				dust.noGravity = true;
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(103, 300);
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 300);
		Vector2 launchVel = base.Projectile.Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, 9f, ignoreKBImmune: true);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/HighResFoggyCircleHardEdge", (AssetRequestMode)2).Value;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/SoftRoundExplosion", (AssetRequestMode)2).Value;
		Color drawColor2 = Color.LightBlue;
		float rotMult = (CalamityClientConfig.Instance.Photosensitivity ? 0f : 1f);
		float opacityMult = (CalamityClientConfig.Instance.Photosensitivity ? 0.33f : 1f);
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color val = drawColor2;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(tex, position, null, val * opacityMult, 0f, tex.Size() / 2f, 0.2f * fade * areaScale, (SpriteEffects)0);
		Vector2 position2 = base.Projectile.Center - Main.screenPosition;
		val = drawColor2;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(tex2, position2, null, val * 0.3f * opacityMult, base.Projectile.rotation * rotMult, tex2.Size() / 2f, 0.2f * fade * areaScale, (SpriteEffects)0);
		Vector2 position3 = base.Projectile.Center - Main.screenPosition;
		val = drawColor2;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(tex2, position3, null, val * 0.3f * opacityMult, (0f - base.Projectile.rotation) * rotMult, tex2.Size() / 2f, 0.2f * fade * areaScale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 200f * areaScale, targetHitbox);
	}
}
