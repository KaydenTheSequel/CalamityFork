using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EnchantedAxeProj : ModProjectile, ILocalizedModType, IModType
{
	private bool recall;

	private bool summonAxe = true;

	private int Lifetime = 600;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/EnchantedAxe";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (base.Projectile.timeLeft < Lifetime - 18)
			{
				recall = true;
				base.Projectile.tileCollide = false;
			}
		}
		else if (base.Projectile.timeLeft < Lifetime - 10)
		{
			recall = true;
			base.Projectile.tileCollide = false;
		}
		base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		if (recall)
		{
			Vector2 posDiff = Main.player[base.Projectile.owner].position - base.Projectile.position;
			if (((Vector2)(ref posDiff)).Length() > 30f)
			{
				((Vector2)(ref posDiff)).Normalize();
				base.Projectile.velocity = posDiff * 30f;
			}
			else
			{
				base.Projectile.timeLeft = 0;
				OnKill(base.Projectile.timeLeft);
			}
			if (summonAxe)
			{
				SummonAxe(recall: true);
			}
		}
		else if (base.Projectile.timeLeft % 3 == 1 && base.Projectile.Calamity().stealthStrike)
		{
			SummonAxe(recall: false);
		}
		if (base.Projectile.position == Main.player[base.Projectile.owner].position)
		{
			OnKill(base.Projectile.timeLeft);
		}
	}

	public void SummonAxe(bool recall)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
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
		Vector2 newAxeVelocity = ((!(minDist < 999f)) ? (-base.Projectile.velocity) : (Main.npc[index].Center - base.Projectile.Center));
		((Vector2)(ref newAxeVelocity)).Normalize();
		newAxeVelocity *= 20f;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position, newAxeVelocity, ModContent.ProjectileType<EnchantedAxe2>(), base.Projectile.damage, 2f, base.Projectile.owner, recall ? 0f : 1f);
		if (recall)
		{
			summonAxe = false;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (recall)
		{
			return false;
		}
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		recall = true;
		base.Projectile.tileCollide = false;
		return false;
	}
}
