using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DeathstareEyeball : ModProjectile, ILocalizedModType, IModType
{
	public float PupilScale = 1f;

	public const int BeamFireRate = 60;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center.MinionHoming(720f, Owner, CalamityPlayer.areThereAnyDamnBosses);
		}
	}

	public ref float Time => ref base.Projectile.ai[1];

	public ref float PupilAngle => ref base.Projectile.localAI[0];

	public ref float PupilOutwardness => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color blue = Color.Blue;
		Lighting.AddLight(center, ((Color)(ref blue)).ToVector3());
		bool num = base.Projectile.type == ModContent.ProjectileType<DeathstareEyeball>();
		CalamityPlayer modPlayer = Owner.Calamity();
		Owner.AddBuff(ModContent.BuffType<MiniatureEyeofCthulhu>(), 3600);
		if (num)
		{
			if (Owner.dead)
			{
				modPlayer.deathstareEyeball = false;
			}
			if (modPlayer.deathstareEyeball)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		Vector2 destination = Owner.Center + Vector2.UnitY * (Owner.gfxOffY - 110f);
		if (Owner.gravDir == -1f)
		{
			destination.Y += 220f;
		}
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, destination, 0.36f);
		base.Projectile.position = base.Projectile.position.Floor();
		base.Projectile.rotation = (base.Projectile.position.X - base.Projectile.oldPosition.X) * 0.07f + Owner.velocity.X * 0.2f;
		base.Projectile.rotation = Utils.Clamp(base.Projectile.rotation, -0.4f, 0.4f);
		if (base.Projectile.ai[0] == 0f)
		{
			Initialize(Owner);
			base.Projectile.ai[0] = 1f;
		}
		if (Target == null)
		{
			DoHoveringAI();
		}
		else
		{
			DoAttackingAI(Target);
		}
		base.Projectile.frame = (int)(Time / 5f) % 4;
		Time++;
	}

	public void DoHoveringAI()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (Time % 180f > 120f && Math.Abs(base.Projectile.rotation) < 0.03f)
		{
			float idealAngle = Utils.GetLerpValue(120f, 180f, Time % 180f, clamped: true) * ((float)Math.PI * 2f);
			PupilAngle = PupilAngle.AngleTowards(idealAngle, MathHelper.ToRadians(12f));
			PupilOutwardness = MathHelper.Lerp(PupilOutwardness, 4f, 0.2f);
		}
		else
		{
			float idealOutwardness = MathHelper.Clamp(((Vector2)(ref Owner.velocity)).Length() * 0.5f, 0f, 5f);
			float idealAngle2 = Owner.velocity.ToRotation();
			PupilOutwardness = MathHelper.Lerp(PupilOutwardness, idealOutwardness, 0.2f);
			PupilAngle = PupilAngle.AngleTowards(idealAngle2, MathHelper.ToRadians(12f));
		}
	}

	public void DoAttackingAI(NPC target)
	{
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		PupilOutwardness = MathHelper.Lerp(PupilOutwardness, 4f, 0.25f);
		if (Time % 60f == 40f)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 velocity = (target.Center - base.Projectile.Center) / 15f;
				int beam = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, velocity, ModContent.ProjectileType<DeathstareBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				if (Main.projectile.IndexInRange(beam))
				{
					Main.projectile[beam].ai[0] = base.Projectile.identity;
					Main.projectile[beam].Damage();
				}
			}
			PupilScale = 1.5f;
		}
		if (Time % 60f > 40f)
		{
			PupilScale = MathHelper.Lerp(PupilScale, 0.7f, 0.15f);
			return;
		}
		PupilScale = MathHelper.Lerp(PupilScale, 1f, 0.175f);
		PupilAngle = PupilAngle.AngleTowards(base.Projectile.AngleTo(target.Center), MathHelper.ToRadians(15f));
	}

	public void Initialize(Player player)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position, 52, 52, 5);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.5f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		_ = Owner.gravDir;
		_ = 1f;
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Texture2D pupilTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/DeathstareEyePupil", (AssetRequestMode)2).Value;
		Vector2 pupilDrawPosition = base.Projectile.Center - Main.screenPosition + PupilAngle.ToRotationVector2() * PupilOutwardness;
		pupilDrawPosition -= Vector2.UnitY * 6f;
		Main.EntitySpriteDraw(pupilTexture, pupilDrawPosition, null, base.Projectile.GetAlpha(lightColor), PupilAngle, pupilTexture.Size() * 0.5f, PupilScale, (SpriteEffects)0);
		return false;
	}
}
