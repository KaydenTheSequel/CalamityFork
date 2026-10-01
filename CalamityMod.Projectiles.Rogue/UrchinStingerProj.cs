using System;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class UrchinStingerProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/UrchinStinger";

	public ref float Timer => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.StickyProjAI(base.Projectile.Calamity().stealthStrike ? 10 : 3);
		Timer++;
		if (base.Projectile.ai[0] == 1f)
		{
			return;
		}
		if (Timer <= 30f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
			return;
		}
		base.Projectile.velocity.Y += 0.3f;
		base.Projectile.velocity.X *= 0.98f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		base.Projectile.rotation += 0.2f * (float)base.Projectile.direction;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[0] != 1f)
		{
			return base.CanDamage();
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.buffImmune[20] = false;
		target.AddBuff(20, base.Projectile.Calamity().stealthStrike ? 600 : 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(20, 180);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = 1f;
		base.Projectile.ai[1] = target.whoAmI;
		base.Projectile.velocity = target.Center - base.Projectile.Center;
		base.Projectile.netUpdate = true;
		Point[] stuckProjArray = (Point[])(object)new Point[7];
		int projCount = 0;
		bool stealthProjStuck = false;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.whoAmI != base.Projectile.whoAmI && Main.myPlayer == p.owner && p.type == base.Projectile.type && p.ai[0] == 1f && p.ai[1] == base.Projectile.ai[1])
			{
				stuckProjArray[projCount++] = new Point(p.whoAmI, p.timeLeft);
				if (p.Calamity().stealthStrike)
				{
					stealthProjStuck = true;
				}
				if (projCount >= stuckProjArray.Length)
				{
					break;
				}
			}
		}
		if (projCount < stuckProjArray.Length)
		{
			return;
		}
		if (stealthProjStuck)
		{
			for (int m = 0; m < stuckProjArray.Length; m++)
			{
				Main.projectile[stuckProjArray[m].X].Kill();
			}
			SoundEngine.PlaySound(in SoundID.Item64, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<UrchinIrradiation>(), (int)((float)base.Projectile.damage * 1.75f), 0f, base.Projectile.owner);
			return;
		}
		int stuckProjAmt = 0;
		for (int i = 1; i < stuckProjArray.Length; i++)
		{
			if (stuckProjArray[i].Y < stuckProjArray[stuckProjAmt].Y)
			{
				stuckProjAmt = i;
			}
		}
		Main.projectile[stuckProjArray[stuckProjAmt].X].Kill();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 100).noGravity = true;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			return Color.Lerp(Color.White, Color.Green, (float)Math.Abs(Math.Sin(Timer * (float)Math.PI / 30f)));
		}
		return null;
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
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
