using System;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PoleWarperSummon : ModProjectile, ILocalizedModType, IModType
{
	public float AngularOffset;

	public const float MaximumRepulsionSpeed = 11f;

	public const float ChargeTime = 45f;

	public new string LocalizationCategory => "Projectiles.Misc";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public bool North
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 0.5f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[0] == 0f)
		{
			Initialize(player);
			base.Projectile.localAI[0] = 1f;
		}
		GrantBuffs(player);
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2000f, player);
		if (!base.Projectile.WithinRange(player.Center, 4200f))
		{
			base.Projectile.Center = player.Center + Vector2.UnitY * (float)North.ToDirectionInt() * 25f;
		}
		if (potentialTarget == null)
		{
			PlayerMovement(player);
			RepelMovement();
		}
		else
		{
			NPCMovement(potentialTarget);
			if (Time % 45f < 35f)
			{
				RepelMovement();
			}
		}
		Time++;
	}

	public void Initialize(Player player)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 45; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f / 45f * (float)i).ToRotationVector2() * 4f;
			Dust.NewDustPerfect(base.Projectile.Center + velocity * 2.75f, 261, velocity).noGravity = true;
		}
	}

	public void GrantBuffs(Player player)
	{
		bool num = base.Projectile.type == ModContent.ProjectileType<PoleWarperSummon>();
		player.AddBuff(ModContent.BuffType<PoleWarperBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				player.Calamity().poleWarper = false;
			}
			if (player.Calamity().poleWarper)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void PlayerMovement(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = player.Center + Vector2.UnitY.RotatedBy(Time / 16f + AngularOffset + (float)(!North).ToInt() * (float)Math.PI) * 180f;
		base.Projectile.velocity = (base.Projectile.velocity * 4f + base.Projectile.SafeDirectionTo(destination) * 10f) / 5f;
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 10f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public void NPCMovement(NPC npc)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == base.Projectile.type && base.Projectile.owner == base.Projectile.owner)
			{
				PoleWarperSummon otherPole = (PoleWarperSummon)p.ModProjectile;
				if (otherPole.Time != Time && otherPole.Time != Time + 1f)
				{
					otherPole.Time = Time;
				}
			}
		}
		if (Time % 45f < 20f)
		{
			float offsetAngle = AngularOffset * 0.5f + (float)(!North).ToInt() * (float)Math.PI;
			Vector2 destination = npc.Center + Vector2.UnitY.RotatedBy(offsetAngle) * 180f;
			base.Projectile.velocity = (base.Projectile.velocity * 4f + base.Projectile.SafeDirectionTo(destination) * 10f) / 5f;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 30f;
			base.Projectile.rotation = base.Projectile.AngleTo(npc.Center) + (float)Math.PI / 2f;
		}
		else if (Time % 45f < 35f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.96f;
			base.Projectile.rotation += 0.05f;
		}
		else if (Time % 45f == 35f)
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(npc.Center, -Vector2.UnitY) * 20f;
			base.Projectile.rotation = base.Projectile.AngleTo(npc.Center) + (float)Math.PI / 2f;
		}
	}

	public void RepelMovement()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == base.Projectile.type && base.Projectile.Distance(p.Center) < 40f && ((PoleWarperSummon)p.ModProjectile).North != North)
			{
				float distanceFromOtherPole = base.Projectile.Distance(p.Center) + 1f;
				if (float.IsNaN(distanceFromOtherPole) || distanceFromOtherPole < 1f)
				{
					distanceFromOtherPole = 1f;
				}
				float repulsionSpeed = 11f * (float)Math.Pow(3.0, (0f - distanceFromOtherPole) / 27f);
				Projectile projectile = base.Projectile;
				projectile.velocity -= (p.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitY) * repulsionSpeed;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				if (i % 2 != 0)
				{
					Color trailColor = Color.Lerp(drawColor, Color.Transparent, (float)i / (float)base.Projectile.oldPos.Length) * 0.67f;
					Vector2 trailPos = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
					Main.EntitySpriteDraw(tex, trailPos, null, trailColor, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
				}
			}
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, drawColor, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
