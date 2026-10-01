using CalamityMod.Packets.Entities;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BlazingStarProj : ModProjectile, ILocalizedModType, IModType
{
	private static int Lifetime = 300;

	private bool hasHit;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/BlazingStar";

	private int timeAlive => Lifetime - base.Projectile.timeLeft;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		Lifetime = 300;
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.penetrate = 6;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = Lifetime;
		base.DrawOffsetX = -10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		base.Projectile.rotation += 0.175f * (float)base.Projectile.direction;
		if (timeAlive == 0)
		{
			base.Projectile.localNPCHitCooldown = -1;
			if (base.Projectile.Calamity().stealthStrike)
			{
				base.Projectile.penetrate = -1;
				base.Projectile.MaxUpdates++;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	private void Ricochet()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		float maxDistance = 1600f;
		float npcDistCompare = 1600f;
		int index = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile) && base.Projectile.WithinRange(n.Center, maxDistance) && base.Projectile.localNPCImmunity[n.whoAmI] == 0)
			{
				float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
				if (currentNPCDist < npcDistCompare && Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1))
				{
					npcDistCompare = currentNPCDist;
					index = n.whoAmI;
				}
			}
		}
		if (index != -1)
		{
			base.Projectile.ai[1] = index;
			base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, Main.npc[index], ((Vector2)(ref base.Projectile.velocity)).Length(), base.Projectile.MaxUpdates);
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (!hasHit)
			{
				target.Calamity().blazingStarShredTimer += 300;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), 0, 0f, base.Projectile.owner, 0f, 0.1f);
				if (Main.netMode != 0)
				{
					GlaiveShredPacket.Send(target);
				}
			}
			hasHit = true;
		}
		Ricochet();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		base.Projectile.ResetLocalNPCHitImmunity();
		Ricochet();
		if (base.Projectile.penetrate > 0)
		{
			base.Projectile.penetrate--;
		}
		return false;
	}
}
