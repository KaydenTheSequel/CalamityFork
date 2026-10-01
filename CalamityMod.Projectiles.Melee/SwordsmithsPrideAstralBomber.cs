using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SwordsmithsPrideAstralBomber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Summon/AureusBomber";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 240;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
			if (base.Projectile.frame >= 3)
			{
				base.Projectile.frame = 0;
			}
		}
		Vector2 center = base.Projectile.Center;
		Color val = Color.Lerp(Color.Orange, Color.Cyan, (float)Math.Sin(Main.GlobalTimeWrappedHourly));
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3());
		if (base.Projectile.timeLeft % 10 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(new Vector2(base.Projectile.position.X + (float)Main.rand.Next(base.Projectile.width), base.Projectile.position.Y + (float)Main.rand.Next(base.Projectile.height)), Vector2.Zero, Main.rand.NextBool() ? Color.Orange : Color.Cyan, Color.White, 0.9f, 30, 0f));
		}
		Vector2 destination = base.Projectile.Center;
		bool foundTarget = false;
		float npcDistCompare = 400f;
		int index = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile))
			{
				float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
				if (currentNPCDist < npcDistCompare)
				{
					npcDistCompare = currentNPCDist;
					index = n.whoAmI;
				}
			}
		}
		if (index != -1)
		{
			destination = Main.npc[index].Center;
			foundTarget = true;
		}
		if (foundTarget)
		{
			Vector2 homeDirection = (destination - base.Projectile.Center).SafeNormalize(Vector2.UnitY);
			base.Projectile.velocity = (base.Projectile.velocity * 20f + homeDirection * 15f) / 21f;
			if ((destination - base.Projectile.Center).X > 0f)
			{
				base.Projectile.spriteDirection = (base.Projectile.direction = -1);
			}
			else
			{
				base.Projectile.spriteDirection = (base.Projectile.direction = 1);
			}
		}
		else if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = -1);
		}
		else if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = 1);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 90);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 9; i++)
		{
			Vector2 veloc = Vector2.UnitY.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(3.5f, 6f);
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, veloc, affectedByGravity: false, 16, 0.8f, Main.rand.NextBool() ? Color.Orange : Color.Cyan));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AureusBomberGlow", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, 3, 0, base.Projectile.frame);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, 0f, frame.Size() / 2f, 1f, (SpriteEffects)0);
	}
}
