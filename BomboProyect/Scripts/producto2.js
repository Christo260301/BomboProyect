const fileInput = document.querySelector("#Fotografia")
const iEliminarImg = document.querySelector("#i-eliminar-img")

let listJsonInsumos = []
let listJsonDetRemove = []
const listUnidades = ['KG', 'GR', 'LT', 'ML', 'PZ']


let cantidadInsumos = 0;

if (fileInput !== null) {
    fileInput.addEventListener("change", e => {
        const img = document.querySelector("#img-preview")
        //const input = document.querySelector("#input-imagen")

        const reader = new FileReader();
        reader.onload = () => {
            fileInput.classList.add("hidden")
            img.setAttribute("src", reader.result)
            img.setAttribute("height", 200)
            //input.setAttribute("value", reader.result)
            iEliminarImg.classList.remove("hidden")
            iEliminarImg.addEventListener("click", e => {
                img.removeAttribute("src")
                img.removeAttribute("height")
                fileInput.value = null
                fileInput.classList.remove("hidden")
                e.target.classList.add("hidden")
            })
        }

        reader.readAsDataURL(e.target.files[0]);
    })
}

// ------------------ * CREATE PRODUCTO * ---------------------- //

const removeImage = () => {
    const img = document.querySelector("#img-preview")
    img.removeAttribute("src")
    img.removeAttribute("style")
    fileInput.value = null
    fileInput.classList.remove("hidden")
    iEliminarImg.classList.add("hidden")
}

const addInsumo = (jsonInsumo, isEdit) => {
    // adicion de insumo
    const ContElmeJson = document.querySelector("#ContElmeJson")
    ContElmeJson.value = JSON.stringify(cantidadInsumos)
    const tBodyContainer = document.querySelector("#tbContainer")
    let NumRow = parseInt(ContElement.value)

    let newNewRow = NumRow + 1

    // TROW
    const trow = document.createElement("tr")

    // Campos completos de insumo
    const tdIdNombre = document.createElement("td")
    const tdUnidad = document.createElement("td")

    const selectIdNombre = document.createElement("select")
    const selectUnidad = document.createElement("select")

    // ID DE INSUMO
    selectIdNombre.setAttribute("name", !isEdit ? `[${NumRow}].InsumoId` : `[${NumRow}].Insumo.InsumoId`)
    selectIdNombre.setAttribute('class', 'form-select')
    jsonInsumo.forEach(x => {
        const option = document.createElement("option")
        option.value = x.InsumoId
        option.text = x.Nombre
        selectIdNombre.insertAdjacentElement("beforeend",option)
    })
    tdIdNombre.insertAdjacentElement("afterbegin", selectIdNombre)


    // UNIDAD DE INSUMO
    selectUnidad.setAttribute("name", !isEdit ? `[${NumRow}].Unidad` : `[${NumRow}].Insumo.Unidad`)
    selectUnidad.setAttribute("class", 'form-select')
    listUnidades.forEach(x => {
        const option = document.createElement("option")
        option.value = x
        option.text = x
        selectUnidad.insertAdjacentElement("beforeend", option)
    })
    tdUnidad.insertAdjacentElement("afterbegin", selectUnidad)

    const tdCant = document.createElement("td")
    const intCant = document.createElement("input")
    intCant.setAttribute("type", "number")
    intCant.setAttribute("name", !isEdit ? `[${NumRow}].CantProduc` : `[${NumRow}].Insumo.CantProduc`)
    intCant.setAttribute("min", "0")
    intCant.setAttribute("value", 0.0)
    intCant.setAttribute("step", "any")
    intCant.setAttribute("class", "form-control")
    tdCant.insertAdjacentElement("afterbegin", intCant)

    const tdAccion = document.createElement("td")
    tdAccion.innerHTML = `
        <div class="d-flex justify-content-start">
            <button type="button" onclick="removeInsumo(${ newNewRow }, true)" class="btn btn-danger">
                <i class="fa-solid fa-minus"></i>
            </button>
         </div>
        `

    trow.insertAdjacentElement("afterbegin", tdAccion)
    trow.insertAdjacentElement("afterbegin", tdUnidad)
    trow.insertAdjacentElement("afterbegin", tdCant)
    trow.insertAdjacentElement("afterbegin", tdIdNombre)

    tBodyContainer.insertAdjacentElement("beforeend", trow)
    trow.setAttribute("id", `ID_${newNewRow}`)
    ContElement.value = newNewRow
}

const removeInsumo = (numId, isEdit) => {

    if (!isEdit) {
        const row = document.querySelector(`#ID_${numId}`)
        row.remove()

        const tBodyContainer = document.querySelector("#tbContainer")
        console.log(tBodyContainer)
        const listRow = tBodyContainer.children
        console.log(listRow)
        for (let i = 0; i < listRow.length; i++) {
            console.log('ITEM => ', listRow[i])
            listRow[i].setAttribute("id", `ID_${i}`)
            listRow[i].children[0].children[0].setAttribute("name", `[${i}].InsumoId`)
            listRow[i].children[1].children[0].setAttribute("name", `[${i}].CantProduc`)
            listRow[i].children[2].children[0].setAttribute("name", `[${i}].Unidad`)
            listRow[i].children[3].children[0].children[0].setAttribute("onclick", `removeInsumo(${i})`)
        }

        const ContElementNum = document.querySelector("#ContElement")
        ContElementNum.value = listRow.length
    } else {
        const row = document.querySelector(`#ID_${numId}`)
        row.remove()

        const tBodyContainer = document.querySelector("#tbContainer")
        console.log(tBodyContainer)
        const listRow = tBodyContainer.children
        console.log(listRow)
        for (let i = 0; i < listRow.length; i++) {
            console.log('ITEM => ', listRow[i])
            listRow[i].setAttribute("id", `ID_${i}`)
            listRow[i].children[0].children[0].setAttribute("name", `[${i}].Insumo.InsumoId`)
            listRow[i].children[1].children[0].setAttribute("name", `[${i}].Insumo.CantProduc`)
            listRow[i].children[2].children[0].setAttribute("name", `[${i}].Insumo.Unidad`)
            listRow[i].children[3].children[0].children[0].setAttribute("onclick", `removeInsumo(${i})`)
        }

        const ContElementNum = document.querySelector("#ContElement")
        ContElementNum.value = listRow.length - 1
    }
}

const removeInsumo2 = (jsonInsumo, isEdit) => {
    // ELIMINACION DEL INSUMO DE LA LISTA JSON
    // console.log(JSON.parse(jsonInsumo.replace(/#/g, '\"')))
    let jsInsu = {}
    try {
        jsInsu = JSON.parse(jsonInsumo.replace(/#/g, '\"'))
    } catch (e) {
        console.log("ERR => ", e)
        jsInsu = jsonInsumo
    }

    const idx = listJsonInsumos.findIndex(x => x == jsInsu.InsumoId);
    if (idx != -1) {

        // AGREGA DETALLE A LA LISTA DE REMOVIDOS
        listJsonDetRemove.push(jsInsu.InsumoId)
        const InsumosRemovidos = document.querySelector("#InsumosRemovidos")
        InsumosRemovidos.value = JSON.stringify(listJsonDetRemove)

        listJsonInsumos.splice(idx, 1)
        const row = document.querySelector(`#ID_${jsInsu.InsumoId}`)
        row.remove()
        // ELIMINA EL ROW DE LA LISTA

        // ESTABLECIMIENTO DE INDICES
        const tBodyContainer = document.querySelector("#tbContainer")
        console.log(tBodyContainer)
        const listRow = tBodyContainer.children
        console.log(listRow)
        console.log(listRow.length)
        if (!isEdit) {
            for (let i = 0; i < listRow.length; i++) {
                console.log('ITEM => ', listRow[i])
                //listRow[i].setAttribute("id", `ID_${jsInsu.InsumoId}`)
                listRow[i].children[0].children[0].setAttribute("name", `[${i}].InsumoId`)
                listRow[i].children[1].children[0].setAttribute("name", `[${i}].CantProduc`)
                listRow[i].children[2].children[0].setAttribute("name", `[${i}].Unidad`)
                listRow[i].children[3].children[0].children[0].setAttribute("onclick", `removeInsumo(${i})`)
            }
        } else {
            for (let i = 0; i < listRow.length; i++) {
                console.log('ITEM => ', listRow[i])
                //listRow[i].setAttribute("id", `ID_${jsInsu.InsumoId}`)
                listRow[i].children[0].children[0].setAttribute("name", `[${i}].Insuom.InsumoId`)
                listRow[i].children[1].children[0].setAttribute("name", `[${i}].Insuom.CantProduc`)
                listRow[i].children[2].children[0].setAttribute("name", `[${i}].Insuom.Unidad`)
                listRow[i].children[3].children[0].children[0].setAttribute("onclick", `removeInsumo(${i})`)
            }
        }


        const ContElementNum = document.querySelector("#ContElement")
        ContElementNum.value = listRow.length

        const CntElementJson = document.querySelector("#ContElmeJson")
        CntElementJson.value = JSON.stringify(listJsonInsumos)
    }
}

const prueba = (event, idRow) => {
    //const row = document.querySelector(`#ID_${infoInsumo}`)
    //row.remove()

    //const tBodyContainer = document.querySelector("#tbContainer")
    //console.log(tBodyContainer)
    //const listRow = tBodyContainer.children
    //console.log(listRow)
    //for (let i = 0; i < listRow.length; i++) {
    //    console.log('ITEM => ', listRow[i])
    //    listRow[i].setAttribute("id", `ID_${infoInsumo}`)
    //    listRow[i].children[0].children[0].setAttribute("name", `[${i}].InsumoId`)
    //    listRow[i].children[1].children[0].setAttribute("name", `[${i}].CantProduc`)
    //    listRow[i].children[2].children[0].setAttribute("name", `[${i}].Unidad`)
    //    listRow[i].children[3].children[0].children[0].setAttribute("onclick", `removeInsumo(${i})`)
    //}
    console.log(event)
    console.log(event, idRow)

    const rowIDtoChange = document.querySelector(`#${idRow}`)
    const value = event.target.value
    rowIDtoChange.setAttribute("id", `ID_${value}`)
}

// ------------------ * EDIT PRODUCTO * ---------------------- //

const DataElement = document.querySelector("#tbContainer")

if (DataElement !== null) {
    const DataTr = DataElement.children
    console.log('DATATR => ', DataTr)
    const ContElementNum = document.querySelector("#ContElement")
    ContElementNum.value = DataTr.length
    for (let i = 0; i < DataTr.length; i++) {
        let td = DataTr[i].children
        let infoTd = td[0].children
        let inputId = infoTd[0].value
        listJsonInsumos.push(parseInt(inputId))
    }
}


const ContElmeJson = document.querySelector("#ContElmeJson")

if (ContElmeJson !== null) {
    ContElmeJson.value = JSON.stringify(listJsonInsumos)
}