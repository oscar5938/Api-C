using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Api.Controllers{
    public abstract class GenericController<Model, Entity> : Controller where Entity : class where Model : Models.Model, new(){
//[Route("[controller]")]

    private readonly IServices<Entity> _repository;
    private readonly IMapper _mapper;


    protected GenericController(IServices<Entity> repository, IMapper mapper){
        _repository=repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(){
        var data = await _repository.GetObjet();
        return View(data);
    }

    public async Task<IActionResult> Get(int? id){
        var model = new Model();
        if(id==null){
            model.ID = 0;
            return PartialView("Get", model);
        }

        Entity entity = await _repository.GetObjetByID(id);
        model = _mapper.Map<Model>(entity);
        return PartialView("Get", model);
    }

    [HttpPost]

    public async Task<IActionResult> Post(Model model){
        if(!ModelState.IsValid) return BadRequest(ModelState);

            Entity entity = _mapper.Map<Entity>(model);
            try{
                if(model.ID>0){
                    await _repository.UpdateObjet(entity);
                }else{
                    await _repository.InsertObjet(entity);
                }
            }catch(Exception e){
                return null;
            }
            return Json(entity);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> Delete(int id){
            var ob_deleted = await _repository.DeleteObjet(id);
            return Ok(ob_deleted);
        }
    }
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   /* private readonly DataContext _context;

    public UserController(IServices<Users> categoryService, DataContext context)
    {
      //  _categoryService = categoryService;
        _context = context;
    }

    // GET /category/GetByProfile/{profileId}
    //[HttpGet("GetByUser/{profileId}")]
    public async Task<IActionResult> Index()
    {
        var category = await _context.Users.GetObjet();
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }*/
}
