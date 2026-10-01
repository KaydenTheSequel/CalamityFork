using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoHydraBody : ModProjectile, ILocalizedModType, IModType
{
	public const float DistanceToCheck = 2800f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int TargetNPCIndex
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 52;
		base.Projectile.height = 86;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		bool num = base.Projectile.type == ModContent.ProjectileType<EndoHydraBody>();
		player.AddBuff(ModContent.BuffType<EndoHydraBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.endoHydra = false;
			}
			if (modPlayer.endoHydra)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		Vector2 returnLocation = player.Center;
		returnLocation.X -= (18 + player.width / 2) * player.direction;
		returnLocation.Y -= 25f;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2800f, player);
		if (potentialTarget != null && TargetNPCIndex != potentialTarget.whoAmI)
		{
			TargetNPCIndex = potentialTarget.whoAmI;
			SoundEngine.PlaySound(in SoundID.Zombie53, base.Projectile.Center);
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.frameCounter++ > ((potentialTarget == null) ? 8 : 6))
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, returnLocation, 0.25f);
		base.Projectile.direction = (base.Projectile.spriteDirection = player.direction);
		Lighting.AddLight(base.Projectile.Center - Vector2.UnitY * 21f, 0.25f, 0.865f, 0.825f);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
