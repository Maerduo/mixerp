/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.EntityParser;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Localization.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "localization.localized_resources".
    /// </summary>
    public class LocalizedResource : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "localization".
        /// </summary>
	    public override string ObjectNamespace => "localization";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "localized_resources".
        /// </summary>
	    public override string ObjectName => "localized_resources";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
		public long LoginId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

		/// <summary>
		/// Performs SQL count on the table "localization.localized_resources".
		/// </summary>
		/// <returns>Returns the number of rows of the table "localization.localized_resources".</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public long Count()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return 0;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT COUNT(*) FROM localization.localized_resources;";
			return Factory.Scalar<long>(this.Catalog, sql);
		}

		/// <summary>
		/// Executes a select query on the table "localization.localized_resources" with a where filter on the column "localized_resource_id" to return a single instance of the "LocalizedResource" class. 
		/// </summary>
		/// <param name="localizedResourceId">The column "localized_resource_id" parameter used on where filter.</param>
		/// <returns>Returns a non-live, non-mapped instance of "LocalizedResource" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public MixERP.Net.Entities.Localization.LocalizedResource Get(long localizedResourceId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the get entity \"LocalizedResource\" filtered by \"LocalizedResourceId\" with value {LocalizedResourceId} was denied to the user with Login ID {LoginId}", localizedResourceId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM localization.localized_resources WHERE localized_resource_id=@0;";
			return Factory.Get<MixERP.Net.Entities.Localization.LocalizedResource>(this.Catalog, sql, localizedResourceId).FirstOrDefault();
		}

        /// <summary>
        /// Custom fields are user defined form elements for localization.localized_resources.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection for the table localization.localized_resources</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get custom fields for entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            string sql;
			if (string.IsNullOrWhiteSpace(resourceId))
            {
				sql = "SELECT * FROM core.custom_field_definition_view WHERE table_name='localization.localized_resources' ORDER BY field_order;";
				return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql);
            }

            sql = "SELECT * from core.get_custom_field_definition('localization.localized_resources'::text, @0::text) ORDER BY field_order;";
			return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql, resourceId);
        }

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of localization.localized_resources.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table localization.localized_resources</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<DisplayField> GetDisplayFields()
		{
			List<DisplayField> displayFields = new List<DisplayField>();

			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return displayFields;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get display field for entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT localized_resource_id AS key, culture_code as value FROM localization.localized_resources;";
			using (NpgsqlCommand command = new NpgsqlCommand(sql))
			{
				using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
				{
					if (table?.Rows == null || table.Rows.Count == 0)
					{
						return displayFields;
					}

					foreach (DataRow row in table.Rows)
					{
						if (row != null)
						{
							DisplayField displayField = new DisplayField
							{
								Key = row["key"].ToString(),
								Value = row["value"].ToString()
							};

							displayFields.Add(displayField);
						}
					}
				}
			}

			return displayFields;
		}

		/// <summary>
		/// Inserts or updates the instance of LocalizedResource class on the database table "localization.localized_resources".
		/// </summary>
		/// <param name="localizedResource">The instance of "LocalizedResource" class to insert or update.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void AddOrEdit(MixERP.Net.Entities.Localization.LocalizedResource localizedResource)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

			if(localizedResource.LocalizedResourceId > 0){
				this.Update(localizedResource, localizedResource.LocalizedResourceId);
				return;
			}
	
			this.Add(localizedResource);
		}

		/// <summary>
		/// Inserts the instance of LocalizedResource class on the database table "localization.localized_resources".
		/// </summary>
		/// <param name="localizedResource">The instance of "LocalizedResource" class to insert.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Add(MixERP.Net.Entities.Localization.LocalizedResource localizedResource)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}. {LocalizedResource}", this.LoginId, localizedResource);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Insert(this.Catalog, localizedResource);
		}

		/// <summary>
		/// Updates the row of the table "localization.localized_resources" with an instance of "LocalizedResource" class against the primary key value.
		/// </summary>
		/// <param name="localizedResource">The instance of "LocalizedResource" class to update.</param>
		/// <param name="localizedResourceId">The value of the column "localized_resource_id" which will be updated.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Update(MixERP.Net.Entities.Localization.LocalizedResource localizedResource, long localizedResourceId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to edit entity \"LocalizedResource\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}. {LocalizedResource}", localizedResourceId, this.LoginId, localizedResource);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Update(this.Catalog, localizedResource, localizedResourceId);
		}

		/// <summary>
		/// Deletes the row of the table "localization.localized_resources" against the primary key value.
		/// </summary>
		/// <param name="localizedResourceId">The value of the column "localized_resource_id" which will be deleted.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public void Delete(long localizedResourceId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"LocalizedResource\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}.", localizedResourceId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "DELETE FROM localization.localized_resources WHERE localized_resource_id=@0;";
			Factory.NonQuery(this.Catalog, sql, localizedResourceId);
		}

		/// <summary>
		/// Performs a select statement on table "localization.localized_resources" producing a paged result of 25.
		/// </summary>
		/// <returns>Returns the first page of collection of "LocalizedResource" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<MixERP.Net.Entities.Localization.LocalizedResource> GetPagedResult()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the first page of the entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}.", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM localization.localized_resources ORDER BY localized_resource_id LIMIT 25 OFFSET 0;";
			return Factory.Get<MixERP.Net.Entities.Localization.LocalizedResource>(this.Catalog, sql);
		}

		/// <summary>
		/// Performs a select statement on table "localization.localized_resources" producing a paged result of 25.
		/// </summary>
		/// <param name="pageNumber">Enter the page number to produce the paged result.</param>
		/// <returns>Returns collection of "LocalizedResource" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
		public IEnumerable<MixERP.Net.Entities.Localization.LocalizedResource> GetPagedResult(long pageNumber)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}.", pageNumber, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			long offset = (pageNumber -1) * 25;
			const string sql = "SELECT * FROM localization.localized_resources ORDER BY localized_resource_id LIMIT 25 OFFSET @0;";
				
			return Factory.Get<MixERP.Net.Entities.Localization.LocalizedResource>(this.Catalog, sql, offset);
		}

        /// <summary>
		/// Performs a filtered select statement on table "localization.localized_resources" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filters">The list of filter conditions.</param>
		/// <returns>Returns collection of "LocalizedResource" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Localization.LocalizedResource> GetWhere(long pageNumber, List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", pageNumber, this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM localization.localized_resources WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Localization.LocalizedResource(), filters);

            sql.OrderBy("localized_resource_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Localization.LocalizedResource>(this.Catalog, sql);
        }

        public IEnumerable<MixERP.Net.Entities.Localization.LocalizedResource> Get(long[] localizedResourceIds)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to entity \"LocalizedResource\" was denied to the user with Login ID {LoginId}. localizedResourceIds: {localizedResourceIds}.", this.LoginId, localizedResourceIds);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

			const string sql = "SELECT * FROM localization.localized_resources WHERE localized_resource_id IN (@0);";

            return Factory.Get<MixERP.Net.Entities.Localization.LocalizedResource>(this.Catalog, sql, localizedResourceIds);
        }
	}
}