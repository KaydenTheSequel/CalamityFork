using System;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SproutingArrowMain : ModProjectile, ILocalizedModType, IModType
{
	private bool hitDirect;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 35;
		base.Projectile.extraUpdates = 80;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 8;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.3f);
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 86f, base.Projectile.velocity * 0.1f, "CalamityMod/Particles/BloomArrow", affectedByGravity: false, 9, 0.7f, Main.rand.NextBool() ? Color.LimeGreen : Color.Lime, new Vector2(0.5f, 4f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.3f), 0.95f, 0.8f));
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.timeLeft == 1)
		{
			for (int index = 0; index < Main.npc.Length; index++)
			{
				NPC nPC = Main.npc[index];
				float generousHitboxWidth = Math.Max((float)nPC.Hitbox.Width / 2f, (float)nPC.Hitbox.Height / 2f);
				if (nPC.Center.Distance(base.Projectile.Center) < 15f + generousHitboxWidth && (nPC.IsAnEnemy(allowStatues: true, checkDead: true, checkDamage: false) || nPC.type == ModContent.NPCType<SuperDummyNPC>()) && nPC.CanBeChasedBy() && !hitDirect)
				{
					float blastSize = 45f;
					float minMultiplier = 0.4f;
					int hitsToMinMult = 4;
					Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), base.Projectile.damage, 0f, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
					projectile.DamageType = DamageClass.Ranged;
					projectile.ArmorPenetration = 8;
					hitDirect = true;
					for (int i = 0; i < 2; i++)
					{
						SoundStyle obj = ((i == 0) ? SoundID.Item53 : SoundID.Item52);
						SoundEngine.PlaySound(obj with
						{
							Pitch = ((i == 0) ? 0.9f : 0.5f),
							Volume = 0.45f,
							MaxInstances = 2
						}, nPC.Center);
					}
					int Dusts = 8;
					float radians = (float)Math.PI * 2f / (float)Dusts;
					Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
					float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
					for (int j = 0; j < Dusts; j++)
					{
						Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)j).RotatedBy(0.5f * rotRando) * 6f;
						Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 264, dustVelocity);
						dust.noGravity = true;
						dust.scale = Main.rand.NextFloat(0.85f, 1.35f);
						dust.color = (Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime);
					}
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 vel1 = (base.Projectile.velocity * 0.4f).RotatedBy(Main.rand.NextFloat(0.015f, 0.04f));
			Vector2 vel2 = (base.Projectile.velocity * 0.4f).RotatedBy(Main.rand.NextFloat(-0.015f, -0.04f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.8999999761581421) * 0.01f, "CalamityMod/Particles/FullStar", affectedByGravity: false, 10, Main.rand.NextFloat(0.8f, 1.1f) * (float)(hitDirect ? 4 : 2), Color.LimeGreen, new Vector2(1f, 0.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 1f, 0.8f, 0.8f));
			Projectile split1 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel1 * Main.rand.NextFloat(0.95f, 1.05f), ModContent.ProjectileType<SproutingArrowSplit>(), base.Projectile.damage * 2, 0f, base.Projectile.owner, 0f, hitDirect ? 1f : 0f);
			Projectile split2 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel2 * Main.rand.NextFloat(0.95f, 1.05f), ModContent.ProjectileType<SproutingArrowSplit>(), base.Projectile.damage * 2, 0f, base.Projectile.owner, 0f, hitDirect ? 1f : 0f);
			if (base.Projectile.Calamity().conditionalHomingRange > 0f)
			{
				split1.Calamity().conditionalHomingRange = base.Projectile.Calamity().conditionalHomingRange;
				split2.Calamity().conditionalHomingRange = base.Projectile.Calamity().conditionalHomingRange;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
	}
}
