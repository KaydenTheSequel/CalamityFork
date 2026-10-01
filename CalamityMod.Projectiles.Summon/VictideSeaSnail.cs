using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VictideSeaSnail : ModProjectile, ILocalizedModType, IModType
{
	public const int frameTime = 6;

	public const int timeToStandStillBeforePeekOut = 200;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float DustTimer => ref base.Projectile.localAI[0];

	public ref float FireCooldown => ref base.Projectile.localAI[1];

	public ref float PeekingOut => ref base.Projectile.ai[0];

	public ref float PlayerStandStillTimer => ref base.Projectile.ai[1];

	public bool CanComePeekOut
	{
		get
		{
			if (Main.player[base.Projectile.owner].mount.Active || !(PlayerStandStillTimer >= 200f))
			{
				return base.Projectile.frame > 0;
			}
			return true;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Projectile.type] = true;
		Main.projFrames[base.Projectile.type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.victideSummoner)
		{
			for (int d = 0; d < 45; d++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 179);
				dust.velocity *= 2f;
				dust.scale *= 1.15f;
			}
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.victideSnail = false;
		}
		if (modPlayer.victideSnail)
		{
			base.Projectile.timeLeft = 2;
		}
		DustTimer++;
		if (DustTimer <= 3f)
		{
			int dustAmount = Main.rand.Next(40, 50);
			for (int i = 0; i < dustAmount; i++)
			{
				Dust dust2 = Dust.NewDustDirect(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 179);
				dust2.velocity *= 2f;
				dust2.scale *= 1.15f;
			}
		}
		if (((Vector2)(ref player.velocity)).Length() == 0f)
		{
			PlayerStandStillTimer++;
			if (PlayerStandStillTimer == 200f)
			{
				DustTimer = 0f;
				float direction = (Main.rand.NextBool() ? 1f : (-1f));
				base.Projectile.velocity = new Vector2(Main.rand.NextFloat(4f, 10f) * direction, Main.rand.NextFloat(-6f, 0f));
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
			base.Projectile.velocity.X *= 0.95f;
			base.Projectile.velocity.Y = Math.Min(base.Projectile.velocity.Y + 0.1f, 14f);
			base.Projectile.tileCollide = true;
			if (CanComePeekOut)
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.10995574f);
			}
			if (PeekingOut > 0f)
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 6)
				{
					base.Projectile.frameCounter = 0;
					base.Projectile.frame = Math.Min(6, base.Projectile.frame + 1);
				}
			}
		}
		if (((Vector2)(ref player.velocity)).Length() > 0f && CanComePeekOut)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame--;
			}
			if (base.Projectile.frame == 0)
			{
				PlayerStandStillTimer = 0f;
			}
		}
		if (!CanComePeekOut)
		{
			PeekingOut = 0f;
			if (((Vector2)(ref player.velocity)).Length() > 0f)
			{
				PlayerStandStillTimer = 0f;
			}
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.tileCollide = false;
			Vector2 desiredPosition = player.Center + Vector2.UnitY * (player.gfxOffY - 60f) * player.gravDir;
			((Vector2)(ref desiredPosition))._002Ector((float)(int)desiredPosition.X, (float)(int)desiredPosition.Y);
			base.Projectile.rotation += (float)Math.PI / 20f;
			base.Projectile.Center = base.Projectile.Center.MoveTowards(desiredPosition, 7f + ((Vector2)(ref player.velocity)).Length());
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (FireCooldown > 0f)
			{
				FireCooldown--;
				return;
			}
			bool foundTarget = false;
			float maxDist = 300f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc = enumerator.Current;
				if (npc.CanBeChasedBy(base.Projectile) && Vector2.Distance(base.Projectile.Center, npc.Center) < maxDist && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
				{
					foundTarget = true;
					break;
				}
			}
			if (foundTarget)
			{
				int projAmt = Main.rand.Next(3, 7);
				Vector2 source = default(Vector2);
				for (int u = 0; u < projAmt; u++)
				{
					((Vector2)(ref source))._002Ector(base.Projectile.Center.X - 4f, base.Projectile.Center.Y);
					Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), source, velocity, ModContent.ProjectileType<UrchinSpike>(), base.Projectile.damage, 1f, base.Projectile.owner);
				}
				SoundEngine.PlaySound(in SoundID.Item42, base.Projectile.position);
				FireCooldown = 60f;
			}
		}
		Vector2 val = base.Projectile.Center - player.Center;
		if (((Vector2)(ref val)).Length() > 400f)
		{
			DustTimer = 0f;
			base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY - 60f) * player.gravDir;
			PlayerStandStillTimer = 0f;
			base.Projectile.frame = 0;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 45; d++)
		{
			Dust dust = Dust.NewDustDirect(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 179);
			dust.velocity *= 2f;
			dust.scale *= 1.15f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (oldVelocity.Y >= 0f)
		{
			PeekingOut = 1f;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		Texture2D value = TextureAssets.Projectile[base.Projectile.type].Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 36 * base.Projectile.frame, 38, 34);
		Main.EntitySpriteDraw(origin: (Vector2)((!CanComePeekOut) ? new Vector2(15f, 23f) : (frame.Size() / 2f)), effects: (SpriteEffects)(Math.Sign(base.Projectile.position.X - owner.position.X) > 0), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: lightColor, rotation: base.Projectile.rotation, scale: base.Projectile.scale);
		return false;
	}
}
