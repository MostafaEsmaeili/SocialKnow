﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SK.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SK.Application.TestValues.Queries.ListTestValue
{
    public class ListTestValueQueryHandler : IRequestHandler<ListTestValueQuery, List<TestValueDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ListTestValueQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TestValueDto>> Handle(ListTestValueQuery request, CancellationToken cancellationToken)
        {
            return await _context.TestValues
                .ProjectTo<TestValueDto>(_mapper.ConfigurationProvider)
                .OrderBy(tv => tv.Id)
                .ToListAsync(cancellationToken);
        }
    }
}