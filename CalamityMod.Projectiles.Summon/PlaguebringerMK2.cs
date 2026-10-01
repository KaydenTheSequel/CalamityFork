using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlaguebringerMK2 : ModProjectile, ILocalizedModType, IModType
{
	public const float DistanceToCheck = 1000.0001f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 38;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		bool num = base.Projectile.type == ModContent.ProjectileType<PlaguebringerMK2>();
		player.AddBuff(ModContent.BuffType<MiniPlaguebringerBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.plaguebringerMK2 = false;
			}
			if (modPlayer.plaguebringerMK2)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1000.0001f, player);
		if (potentialTarget != null)
		{
			int sign = (potentialTarget.Center.X - base.Projectile.Center.X < 0f).ToDirectionInt();
			float x = (125f + 40f * (float)(int)(base.Projectile.ai[1] % 10f)) * (float)sign;
			int y = -160 - 50 * (int)(base.Projectile.ai[1] / 10f);
			Vector2 destination = potentialTarget.Center + new Vector2(x, (float)y);
			if (base.Projectile.Distance(destination) < 6f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.9f;
			}
			else
			{
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(destination) * base.Projectile.Distance(destination) / 36f;
			}
			int timeNeeded = (int)(MathHelper.Lerp(60f, 18f, MathHelper.Clamp(base.Projectile.localAI[1] / 320f, 0f, 1f)) * (player.strongBees ? 0.85f : 1f));
			if (base.Projectile.ai[0] >= (float)timeNeeded && Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 14f, ModContent.ProjectileType<MK2RocketNormal>(), base.Projectile.damage, 3f, base.Projectile.owner);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 11.5f, ModContent.ProjectileType<MK2RocketHoming>(), base.Projectile.damage, 3f, base.Projectile.owner);
				base.Projectile.ai[0] = 0f;
			}
			else
			{
				base.Projectile.ai[0]++;
			}
			base.Projectile.localAI[1]++;
			base.Projectile.direction = (base.Projectile.spriteDirection = -sign);
		}
		else
		{
			base.Projectile.localAI[1] = 0f;
			float x2 = (45f + 35f * (float)(int)(base.Projectile.ai[1] % 10f)) * (float)(-player.direction);
			int y2 = -60 - 50 * (int)(base.Projectile.ai[1] / 10f);
			Vector2 distanceToDestination = player.Center - base.Projectile.Center + new Vector2(x2, (float)y2);
			float distance = ((Vector2)(ref distanceToDestination)).Length();
			if (distance > 10f)
			{
				float speed = 20f;
				if (distance < 50f)
				{
					speed /= 2f;
				}
				Vector2 velocity = distanceToDestination.SafeNormalize((float)base.Projectile.direction * Vector2.UnitX) * speed;
				base.Projectile.velocity = (base.Projectile.velocity * 20f + velocity) / 21f;
				if (distance > 2250f)
				{
					base.Projectile.Center = player.Center;
					base.Projectile.netUpdate = true;
				}
			}
			else
			{
				base.Projectile.direction = player.direction;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9f;
			}
			base.Projectile.direction = (base.Projectile.spriteDirection = player.direction);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}
}
