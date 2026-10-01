using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class LadShark : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, 0, 1).WithOffset(-12f, -12f).WithSpriteDirection(-1);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.aiStyle = 26;
		base.AIType = 197;
	}

	public override void AI()
	{
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.ladShark = false;
		}
		if (modPlayer.ladShark)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (!Main.rand.NextBool(10000) || base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		if (!Main.dedServ)
		{
			int heartCount = Main.rand.Next(20, 31);
			Vector2 velocity = default(Vector2);
			for (int i = 0; i < heartCount; i++)
			{
				((Vector2)(ref velocity))._002Ector((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				((Vector2)(ref velocity)).Normalize();
				velocity.X *= 0.66f;
				int heart = Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.Center, velocity * Main.rand.NextFloat(3f, 5f) * 0.33f, 331, Main.rand.NextFloat(40f, 120f) * 0.01f);
				Main.gore[heart].sticky = false;
				Gore obj = Main.gore[heart];
				obj.velocity *= 5f;
			}
		}
		SoundEngine.PlaySound(in SoundID.Zombie15, base.Projectile.position);
		float radius = 240f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.dontTakeDamage && Vector2.Distance(base.Projectile.Center, npc.Center) <= radius && npc.Calamity().ladHearts <= 0)
			{
				npc.Calamity().ladHearts = CalamityUtils.SecondsToFrames(9f);
			}
		}
		ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Player players = enumerator2.Current;
			if (!players.dead && Vector2.Distance(base.Projectile.Center, players.Center) <= radius && players.Calamity().ladHearts <= 0)
			{
				players.Calamity().ladHearts = CalamityUtils.SecondsToFrames(9f);
			}
		}
	}
}
