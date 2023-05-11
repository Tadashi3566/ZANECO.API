// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ZANECO.API.Application.Common.Interfaces;

public interface IDocumentOcrJob
{
    Task Recognition(DefaultIdType id, CancellationToken cancellationToken);
}