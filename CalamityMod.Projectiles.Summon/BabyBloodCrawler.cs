using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BabyBloodCrawler : ModProjectile, ILocalizedModType, IModType
{
	public int bloodCooldown;

	public float dust;

	private bool _hadSpiderMinion;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 11;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.aiStyle = 26;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.AIType = 390;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30 * base.Projectile.MaxUpdates;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool PreAI()
	{
		Player owner = Main.player[base.Projectile.owner];
		_hadSpiderMinion = owner.spiderMinion;
		owner.spiderMinion = false;
		return true;
	}

	public override void PostAI()
	{
		Main.player[base.Projectile.owner].spiderMinion = _hadSpiderMinion;
	}

	public override void AI()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		if (bloodCooldown > 0)
		{
			bloodCooldown--;
		}
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (dust == 0f)
		{
			int constant = 16;
			for (int i = 0; i < constant; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (constant / 2 - 1)) * ((float)Math.PI * 2f) / (float)constant) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int bloody = Dust.NewDust(val + faceDirection, 0, 0, 5, faceDirection.X * 1f, faceDirection.Y * 1f, 100, default(Color), 1.1f);
				Main.dust[bloody].noGravity = true;
				Main.dust[bloody].noLight = true;
				Main.dust[bloody].velocity = faceDirection;
			}
			dust++;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<BabyBloodCrawler>();
		player.AddBuff(ModContent.BuffType<BabyBloodCrawlerBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.scabRipper = false;
			}
			if (modPlayer.scabRipper)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffects(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffects(target);
	}

	private void OnHitEffects(Entity target)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (bloodCooldown == 0)
		{
			bloodCooldown = 15;
			Vector2 spawnPos = target.Center + Vector2.UnitX * Main.rand.NextFloat(-100f, 100f) - Vector2.UnitY * Main.rand.NextFloat(400f, 700f);
			Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(spawnPos, target, 25f, 2);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, velocity, ModContent.ProjectileType<BloodRain>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
