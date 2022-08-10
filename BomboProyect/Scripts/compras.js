let listJsonInsumos = []
let listJsonDetRemove = []

const addInsumo = (jsonInsumo, isEdit) => {
    console.log(jsonInsumo)

    if (listJsonInsumos.find(x => x == jsonInsumo.InsumoId) == undefined) {
        listJsonInsumos.push(jsonInsumo.InsumoId)

        const idxR = listJsonDetRemove.findIndex(x => x == jsonInsumo.InsumoID)
        if (idxR != -1) {
            listJsonDetRemove.splice(idxR, 1)
            const InsumosRemovidos = document.querySelector('#InsumosRemovidos')
            InsumosRemovidos.value = JSON.stringify(listJsonDetRemove)
        }

        console.log(jsonInsumo);
        const ContElement = document.querySelector("#ContElement")
        const ContElmeJson = document.querySelector("#ContElmeJson")
        ContElmeJson.value = JSON.stringify(listJsonInsumos)
        const tBodyContainer = document.querySelector("#tbContainer")
        let NumRow = parseInt(ContElement.value)

        console.log(ContElement)

        const trow = document.createElement("tr")
        const tdDataIsumo = document.createElement("td")
        tdDataIsumo.setAttribute("hidden", true)

        const intId = document.createElement("input")
        const intNombre = document.createElement("input")
        const intDescripcion = document.createElement("input")
        const intPrecio = document.createElement("input")
        const intUnidad = document.createElement("input")
        const intCantidadNeta = document.createElement("input")
        const intContenidoTot = document.createElement("input")
        const intExistencias = document.createElement("input")
        const intStatus = document.createElement("input")


        // STATUS DEL INSUMO
        intStatus.setAttribute("name", !isEdit ? `[${NumRow}].Status` : `[${NumRow}].Insumo.Status`)
        intStatus.setAttribute("value", jsonInsumo.Status)
        intStatus.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intStatus)

        // CANTIDADTOT DEL INSUMO
        intContenidoTot.setAttribute("name", !isEdit ? `[${NumRow}].ContenidoTot` : `[${NumRow}].Insumo.ContenidoTot`)
        intContenidoTot.setAttribute("value", jsonInsumo.ContenidoTot)
        intContenidoTot.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intContenidoTot)

        // EXISTENCIAS DEL INSUMO
        intExistencias.setAttribute("name", !isEdit ? `[${NumRow}].Existencias` : `[${NumRow}].Insumo.Existencias`)
        intExistencias.setAttribute("value", jsonInsumo.Existencias)
        intExistencias.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intExistencias)

        // CANTIDAD NETA DEL INSUMO
        intCantidadNeta.setAttribute("name", !isEdit ? `[${NumRow}].CantidadNeta` : `[${NumRow}].Insumo.CantidadNeta`)
        intCantidadNeta.setAttribute("value", jsonInsumo.CantidadNeta)
        intCantidadNeta.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intCantidadNeta)

        // UNIDAD DE INSUMO
        intUnidad.setAttribute("name", !isEdit ? `[${NumRow}].Unidad` : `[${NumRow}].Insumo.Unidad`)
        intUnidad.setAttribute("value", jsonInsumo.Unidad)
        intUnidad.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intUnidad)

        // PRECION DE INSUMO
        intPrecio.setAttribute("name", !isEdit ? `[${NumRow}].Precio` : `[${NumRow}].Insumo.Precio`)
        intPrecio.setAttribute("value", jsonInsumo.Precio)
        intPrecio.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intPrecio)

        // DESCRIPCION DE INSUMO 
        intDescripcion.setAttribute("name", !isEdit ? `[${NumRow}].Descripcion` : `[${NumRow}].Insumo.Descripcion`)
        intDescripcion.setAttribute("value", jsonInsumo.Descripcion)
        intDescripcion.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intDescripcion)

        // NOMBRE DE INSUMO
        intNombre.setAttribute("name", !isEdit ? `[${NumRow}].Nombre` : `[${NumRow}].Insumo.Nombre`)
        intNombre.setAttribute("value", jsonInsumo.Nombre)
        intNombre.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intNombre)

        // ID DE INSUMO
        intId.setAttribute("name", !isEdit ? `[${NumRow}].InsumoId` : `[${NumRow}].Insumo.InsumoId`)
        intId.setAttribute("value", jsonInsumo.InsumoId)
        intId.setAttribute("type", "hidden")
        tdDataIsumo.insertAdjacentElement("afterbegin", intId)

        const tdNom = document.createElement("td")
        tdNom.innerText = jsonInsumo.Nombre
    
        const tdCant = document.createElement("td")
        const intCant = document.createElement("input")
        intCant.setAttribute("type", "number")
        intCant.setAttribute("name", !isEdit ? `[${NumRow}].CantProduc` : `[${NumRow}].Insumo.CantProduc`)
        intCant.setAttribute("min", "0")
        intCant.setAttribute("value", 0.0)
        intCant.setAttribute("step", "any")
        intCant.setAttribute("class", "form-control")
        tdCant.insertAdjacentElement("afterbegin", intCant)

        const tdFecha = document.createElement("td")
        const intFecha = document.createElement("input")
        intFecha.setAttribute("type", "date")
        intFecha.setAttribute("name", `[${NumRow}].FechaCad`)
        intFecha.setAttribute("value", "")
        intFecha.setAttribute("class", "form-control")
        tdFecha.insertAdjacentElement("afterbegin", intFecha)

        const tdAccion = document.createElement("td")
        tdAccion.innerHTML = `
        <div class="d-flex justify-content-start">
            <button type="button" onclick="removeInsumo(\'${JSON.stringify(jsonInsumo).replace(/"/g, "#")}\')" class="btn btn-danger">
                <i class="fa-solid fa-minus"></i>
            </button>
         </div>
        `

        trow.setAttribute("id", `ID_${jsonInsumo.InsumoId}_${jsonInsumo.Nombre}`)

        trow.insertAdjacentElement("afterbegin", tdAccion)
        trow.insertAdjacentElement("afterbegin", tdCant)
        trow.insertAdjacentElement("afterbegin", tdFecha)
        trow.insertAdjacentElement("afterbegin", tdNom)
        trow.insertAdjacentElement("afterbegin", tdDataIsumo)

        tBodyContainer.insertAdjacentElement("beforeend", trow)
        ContElement.value = (NumRow + 1)
        
    }
}