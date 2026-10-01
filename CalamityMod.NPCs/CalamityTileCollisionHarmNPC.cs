using System;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public class CalamityTileCollisionHarmNPC : GlobalNPC
{
	private int potentialEnergyDamage;

	private Vector2 OldVelocity;

	private Vector2 OlderVelocity;

	private float terminalVelocityForFullFallDamage;

	private bool CheckTiles;

	private Vector2 ForcedVel;

	private bool ForceVelocityOnly;

	private bool hitVoid;

	private Player attacker;

	private int packetTimer;

	public int PotentialEnergyDamage
	{
		get
		{
			return potentialEnergyDamage;
		}
		private set
		{
			potentialEnergyDamage = ((value != 0) ? Math.Max(potentialEnergyDamage, value) : 0);
		}
	}

	public bool FallDamageSusceptible
	{
		get
		{
			if (PotentialEnergyDamage <= 0)
			{
				return ForceVelocityOnly;
			}
			return true;
		}
	}

	public override bool InstancePerEntity => true;

	public override GlobalNPC Clone(NPC npc, NPC npcClone)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		CalamityTileCollisionHarmNPC obj = (CalamityTileCollisionHarmNPC)base.Clone(npc, npcClone);
		obj.potentialEnergyDamage = potentialEnergyDamage;
		obj.OldVelocity = OldVelocity;
		obj.OlderVelocity = OlderVelocity;
		obj.terminalVelocityForFullFallDamage = terminalVelocityForFullFallDamage;
		return obj;
	}

	public override void SetDefaults(NPC npc)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		PotentialEnergyDamage = 0;
		OldVelocity = Vector2.Zero;
		OlderVelocity = Vector2.Zero;
	}

	public void ApplyCollisionDamage(NPC npc, Player player, int potentialDamage, Vector2 forcedVel, float terminalVelocityForFullDamage = 10f, bool checkTiles = false)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (!npc.noTileCollide)
		{
			PotentialEnergyDamage = potentialDamage;
			OldVelocity = npc.velocity;
			OlderVelocity = npc.velocity;
			terminalVelocityForFullFallDamage = terminalVelocityForFullDamage;
			CheckTiles = checkTiles;
			ForcedVel = forcedVel;
			attacker = player;
		}
	}

	public void ApplyForcedVelocity(NPC npc, Player player, Vector2 forcedVel, bool checkTiles = false)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (!npc.noTileCollide)
		{
			OldVelocity = npc.velocity;
			OlderVelocity = npc.velocity;
			CheckTiles = checkTiles;
			ForcedVel = forcedVel;
			attacker = player;
			ForceVelocityOnly = true;
		}
	}

	public override void PostAI(NPC npc)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		if (!FallDamageSusceptible)
		{
			return;
		}
		if (CheckTiles)
		{
			float oldVelocity = ((Vector2)(ref OldVelocity)).Length();
			npc.Center += ForcedVel;
			ForcedVel *= 0.995f;
			if (Collision.SolidCollision(npc.Center, (int)((float)npc.width * 0.5f), (int)((float)npc.height * 0.5f)) || !WorldGen.InWorld(npc.Center.ToTileCoordinates().X, npc.Center.ToTileCoordinates().Y, 40))
			{
				if (!ForceVelocityOnly)
				{
					int wallImpactDamage = (int)((float)PotentialEnergyDamage * Math.Clamp(oldVelocity / terminalVelocityForFullFallDamage, (((Vector2)(ref ForcedVel)).Length() > 0f) ? 1f : 0f, 1f));
					Projectile projectile = Projectile.NewProjectileDirect(attacker.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), wallImpactDamage, 0f, attacker.whoAmI, npc.whoAmI);
					projectile.DamageType = DamageClass.Melee;
					projectile.CritChance = (int)attacker.GetCritChance(DamageClass.Melee) + attacker.HeldItem.crit;
				}
				PotentialEnergyDamage = 0;
				ForceVelocityOnly = false;
				hitVoid = false;
			}
			if (packetTimer % 30 == 0)
			{
				npc.SyncMotionToServer();
			}
			if (!WorldGen.InWorld(npc.Center.ToTileCoordinates().X, npc.Center.ToTileCoordinates().Y, 40) && !hitVoid)
			{
				npc.velocity = -npc.velocity * 0.5f;
				ForcedVel = -ForcedVel * 0.5f;
				hitVoid = true;
			}
			packetTimer++;
			return;
		}
		float y = npc.velocity.Y;
		float oldVerticalVelocity = OldVelocity.Y;
		float olderVerticalVelocity = OlderVelocity.Y;
		if (y < 0f && oldVerticalVelocity >= 0f)
		{
			PotentialEnergyDamage = 0;
		}
		if (y > 0f && olderVerticalVelocity > 0f && oldVerticalVelocity < olderVerticalVelocity)
		{
			PotentialEnergyDamage = 0;
		}
		if (y == 0f && oldVerticalVelocity > 0f)
		{
			if (!ForceVelocityOnly)
			{
				int impactDamage = (int)((float)PotentialEnergyDamage * Math.Clamp(olderVerticalVelocity / terminalVelocityForFullFallDamage, 0f, 1f));
				Projectile.NewProjectileDirect(attacker.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), impactDamage, 0f, attacker.whoAmI, npc.whoAmI).DamageType = DamageClass.Melee;
			}
			PotentialEnergyDamage = 0;
			ForceVelocityOnly = false;
		}
		OlderVelocity = OldVelocity;
		OldVelocity = npc.velocity;
	}

	public CalamityTileCollisionHarmNPC()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		OldVelocity = Vector2.Zero;
		OlderVelocity = Vector2.Zero;
		ForcedVel = Vector2.Zero;
		base._002Ector();
	}
}
