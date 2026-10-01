using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class VeneratedKnife : ModProjectile, ILocalizedModType, IModType
{
	private int lifetime = 120;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft >= lifetime - 5)
		{
			return;
		}
		float minDist = 999f;
		int index = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy(base.Projectile))
			{
				Vector2 val = base.Projectile.Center - npc.Center;
				float dist = ((Vector2)(ref val)).Length();
				if (dist < minDist)
				{
					minDist = dist;
					index = npc.whoAmI;
				}
			}
		}
		if (minDist < 999f)
		{
			Vector2 velocityNew = Main.npc[index].Center - base.Projectile.Center;
			((Vector2)(ref velocityNew)).Normalize();
			velocityNew *= 5f;
			Projectile projectile = base.Projectile;
			projectile.velocity += velocityNew;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 14f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 14f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			Texture2D knife1 = TextureAssets.Projectile[base.Type].Value;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 3, knife1);
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			Texture2D knife2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/VeneratedKnife2", (AssetRequestMode)2).Value;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 3, knife2);
		}
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(base.Projectile.width / 2), (float)(base.Projectile.height / 2));
		if (base.Projectile.ai[0] == 0f)
		{
			Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/VeneratedKnifeGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/VeneratedKnife2Glow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 150);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustType = 0;
			if (base.Projectile.ai[0] == 0f)
			{
				dustType = 111;
			}
			else if (base.Projectile.ai[0] == 1f)
			{
				dustType = 112;
			}
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
		}
	}
}
