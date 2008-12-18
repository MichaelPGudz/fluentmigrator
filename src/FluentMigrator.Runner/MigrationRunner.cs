﻿using System;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Processors;

namespace FluentMigrator.Runner
{
	public class MigrationRunner
	{
		public MigrationConventions Conventions { get; private set; }
		public IMigrationProcessor Processor { get; private set; }

		public MigrationRunner(MigrationConventions conventions, IMigrationProcessor processor)
		{
			Conventions = conventions;
			Processor = processor;
		}

		public void Up(IMigration migration)
		{
			var context = new MigrationContext(Conventions);
			migration.GetUpExpressions(context);

			// BeginTransaction

			foreach (IMigrationExpression expression in context.Expressions)
				expression.ExecuteWith(Processor);

			// CommitTransaction
		}

		public void Down(IMigration migration)
		{
			var context = new MigrationContext(Conventions);
			migration.GetDownExpressions(context);

			// BeginTransaction

			foreach (IMigrationExpression expression in context.Expressions)
				expression.ExecuteWith(Processor);

			// CommitTransaction
		}
	}
}